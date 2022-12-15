using ETConfiguration.Core.Database.Entities;
using ETConfiguration.Core.Database.Repositories;
using ETConfiguration.Core.Database.Sources;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

namespace ETConfiguration.Core.Database.Extensions
{
    public static class DatabaseConfigurationExtension
    {
        public static IConfigurationBuilder AddDatabaseConfiguration(this IConfigurationBuilder builder, 
            IReadConfigurationRepository readConfigurationRepository,
            bool reloadOnChange, 
            int reloadDelay,
            Expression<Func<Configuration, bool>>? configurationExpression = null,
            bool isAsyncProvider = false
            )
        {
            builder.Add(new DatabaseConfigurationSource(readConfigurationRepository, reloadOnChange, reloadDelay, configurationExpression, isAsyncProvider));

            return builder;
        }
    }
}
