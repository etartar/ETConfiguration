using Consul;
using ETConfiguration.Core.Consul.Exceptions;
using Microsoft.Extensions.Configuration;

namespace ETConfiguration.Core.Consul.Sources
{
    public interface IConsulConfigurationSource : IConfigurationSource
    {
        Action<ConsulClientConfiguration>? ConsulConfigurationOptions { get; set; }

        Action<HttpClientHandler>? ConsulHttpClientHandlerOptions { get; set; }

        Action<HttpClient>? ConsulHttpClientOptions { get; set; }

        string Key { get; }

        Action<ConsulLoadException>? OnLoadException { get; set; }

        Func<ConsulWatchException, TimeSpan>? OnWatchException { get; set; }

        TimeSpan ReloadOnChangeWaitTime { get; set; }

        bool ReloadOnChange { get; set; }
    }
}
