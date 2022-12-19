using ETConfiguration.Core.Consul.Sources;
using Microsoft.Extensions.Configuration;

namespace ETConfiguration.Core.Consul.Extensions
{
    public static class ConsulConfigurationExtension
    {
        public static IConfigurationBuilder AddConsulConfiguration(this IConfigurationBuilder builder, string key, Action<IConsulConfigurationSource> options)
        {
            var consulConfigSource = new ConsulConfigurationSource(key);
            options(consulConfigSource);
            builder.Add(consulConfigSource);

            return builder;
        }
    }
}
