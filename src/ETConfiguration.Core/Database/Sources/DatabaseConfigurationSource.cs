using ETConfiguration.Core.Database.Entities;
using ETConfiguration.Core.Database.Providers;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

namespace ETConfiguration.Core.Database.Sources
{
    public class DatabaseConfigurationSource : IConfigurationSource
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly bool _reloadOnChange;
        private readonly int _reloadDelay;
        private readonly Expression<Func<Configuration, bool>>? _predicate = null;

        public DatabaseConfigurationSource(IServiceProvider serviceProvider, bool reloadOnChange, int reloadDelay, Expression<Func<Configuration, bool>>? predicate = null)
        {
            _serviceProvider = serviceProvider;
            _reloadOnChange = reloadOnChange;
            _reloadDelay = reloadDelay;
            _predicate = predicate;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new DatabaseConfigurationProvider(_serviceProvider, _reloadOnChange, _reloadDelay, _predicate);
        }
    }
}
