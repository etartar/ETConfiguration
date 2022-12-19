using Consul;
using ETConfiguration.Core.Consul.Exceptions;
using ETConfiguration.Core.Consul.Factories;
using ETConfiguration.Core.Consul.Sources;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace ETConfiguration.Core.Consul.Providers
{
    public class ConsulConfigurationProvider : ConfigurationProvider, IDisposable
    {
        private readonly IConsulClientFactory _consulClientFactory;
        private readonly IConsulConfigurationSource _source;
        private readonly Dictionary<string, string> _configValues;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private Task? _reloadTask;
        private bool _disposed;

        public ConsulConfigurationProvider(IConsulClientFactory consulClientFactory, IConsulConfigurationSource source)
        {
            _consulClientFactory = consulClientFactory;
            _source = source;
            _configValues = new Dictionary<string, string>();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void Dispose()
        {
            if (_disposed) return;

            _cancellationTokenSource.Cancel();
            _cancellationTokenSource?.Dispose();
            _disposed = true;
        }

        public override void Load()
        {
            if (_reloadTask != null) return;

            CancellationToken cancellationToken = _cancellationTokenSource.Token;

            DoLoad(cancellationToken).GetAwaiter().GetResult();

            if (_source.ReloadOnChange)
            {
                _reloadTask = Task.Run(() => ReloadConfigs(cancellationToken), cancellationToken);
            }
        }

        private async Task DoLoad(CancellationToken cancellationToken)
        {
            try
            {
                await ReadConfigs(cancellationToken);
            }
            catch (Exception ex)
            {
                _source.OnLoadException?.Invoke(new ConsulLoadException(ex));
                throw;
            }
        }

        private async Task ReloadConfigs(CancellationToken cancellationToken = default)
        {
            int failureCount = 0;
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(_source.ReloadOnChangeWaitTime, cancellationToken);
                    await ReadConfigs(cancellationToken);
                    OnReload();
                    failureCount = 0;
                }
                catch (Exception e)
                {
                    ++failureCount;
                    _source.OnWatchException?.Invoke(new ConsulWatchException(e, failureCount));
                    await Task.Delay(TimeSpan.FromSeconds(30));
                }
            }
        }

        private async Task ReadConfigs(CancellationToken cancellationToken)
        {
            _configValues.Clear();

            var getPair = await GetKVPairs(cancellationToken);

            if (getPair?.Response != null)
            {
                string value = Encoding.UTF8.GetString(getPair.Response.Value, 0, getPair.Response.Value.Length);

                DeserializeToDictionary(value);
            }

            Data = _configValues;
        }

        private async Task<QueryResult<KVPair>> GetKVPairs(CancellationToken cancellationToken)
        {
            using var client = _consulClientFactory.Create();
            var queryOptions = new QueryOptions()
            {
                WaitTime = TimeSpan.FromSeconds(5),
                WaitIndex = 0
            };

            QueryResult<KVPair> result = await client.KV.Get(_source.Key, queryOptions, cancellationToken).ConfigureAwait(false);
            return result;
        }

        private void DeserializeToDictionary(string objectValue, string key = "")
        {
            var values = JsonConvert.DeserializeObject<Dictionary<string, object>>(objectValue);

            foreach (KeyValuePair<string, object> d in values)
            {
                if (d.Value is JObject)
                {
                    string delimiter = string.IsNullOrEmpty(key) ? "" : ":";
                    string newKey = $"{key}{delimiter}{d.Key}";
                    DeserializeToDictionary(d.Value.ToString(), newKey);
                }
                else
                {
                    string newKey = string.IsNullOrEmpty(key) ? d.Key : $"{key}:{d.Key}";
                    _configValues.Add(newKey, Convert.ToString(d.Value));
                }
            }
        }
    }
}
