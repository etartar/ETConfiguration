using Consul;
using ETConfiguration.Core.Consul.Exceptions;
using ETConfiguration.Core.Consul.Factories;
using ETConfiguration.Core.Consul.Providers;
using Microsoft.Extensions.Configuration;

namespace ETConfiguration.Core.Consul.Sources
{
    public class ConsulConfigurationSource : IConsulConfigurationSource
    {
        public ConsulConfigurationSource(string key, bool reloadOnChange = false, TimeSpan reloadOnChangeWaitTime = default(TimeSpan))
        {
            Key = key;
            ReloadOnChange = reloadOnChange;
            ReloadOnChangeWaitTime = reloadOnChangeWaitTime;
        }
        public Action<ConsulClientConfiguration>? ConsulConfigurationOptions { get; set; }
        public Action<HttpClientHandler>? ConsulHttpClientHandlerOptions { get; set; }
        public Action<HttpClient>? ConsulHttpClientOptions { get; set; }
        public string Key { get; }
        public Action<ConsulLoadException>? OnLoadException { get; set; }
        public Func<ConsulWatchException, TimeSpan>? OnWatchException { get; set; }
        public TimeSpan ReloadOnChangeWaitTime { get; set; }
        public bool ReloadOnChange { get; set; }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            var factory = new ConsulClientFactory(this);
            return new ConsulConfigurationProvider(factory, this);
        }
    }
}
