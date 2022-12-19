using Consul;
using ETConfiguration.Core.Consul.Sources;

namespace ETConfiguration.Core.Consul.Factories
{
    public class ConsulClientFactory : IConsulClientFactory
    {
        private readonly IConsulConfigurationSource _source;

        public ConsulClientFactory(IConsulConfigurationSource source)
        {
            _source = source;
        }

        public IConsulClient Create()
        {
            return new ConsulClient(_source.ConsulConfigurationOptions,
                _source.ConsulHttpClientOptions,
                _source.ConsulHttpClientHandlerOptions);
        }
    }
}
