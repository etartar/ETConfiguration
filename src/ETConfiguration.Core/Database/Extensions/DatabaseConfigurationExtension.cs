using ETConfiguration.Core.Database.Entities;
using ETConfiguration.Core.Database.Sources;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

namespace ETConfiguration.Core.Database.Extensions
{
    public static class DatabaseConfigurationExtension
    {
        public static IConfigurationBuilder AddDatabaseConfiguration(this IConfigurationBuilder builder, 
            IServiceProvider serviceProvider,
            bool reloadOnChange, 
            TimeSpan reloadDelay,
            Expression<Func<Configuration, bool>>? configurationExpression = null
            )
        {
            builder.Add(new DatabaseConfigurationSource(serviceProvider, reloadOnChange, reloadDelay, configurationExpression));

            return builder;
        }
    }
}
