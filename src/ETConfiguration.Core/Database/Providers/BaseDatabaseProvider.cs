using ETConfiguration.Core.Database.Entities;
using ETConfiguration.Core.Database.Observers;
using ETConfiguration.Core.Database.Repositories;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

namespace ETConfiguration.Core.Database.Providers
{
    public class BaseDatabaseProvider : ConfigurationProvider
    {
        protected readonly IReadConfigurationRepository _repository;
        protected readonly bool _reloadOnChange;
        protected readonly int _reloadDelay;
        protected readonly Expression<Func<Configuration, bool>>? _predicate = null;

        public BaseDatabaseProvider(IReadConfigurationRepository repository, bool reloadOnChange, int reloadDelay, Expression<Func<Configuration, bool>>? predicate)
        {
            _repository = repository;
            _reloadOnChange = reloadOnChange;
            _reloadDelay = reloadDelay;
            _predicate = predicate;

            if (_reloadOnChange)
            {
                EntityChangeObserver.Instance.ChangedConfiguration += EntityChangeObserver_ChangedConfiguration;
            }
        }

        private void EntityChangeObserver_ChangedConfiguration(object? sender, Type e)
        {
            if (e != typeof(Configuration))
                return;

            Thread.Sleep(_reloadDelay);
            Load();
        }
    }
}
