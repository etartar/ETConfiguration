using Consul;

namespace ETConfiguration.Core.Consul.Factories
{
    public interface IConsulClientFactory
    {
        IConsulClient Create();
    }
}
