using ETConfiguration.Core.Database.Entities;
using ETConfiguration.Core.Database.Observers;
using ETConfiguration.Core.Database.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System.Linq.Expressions;

namespace ETConfiguration.Core.Database.Providers
{
    public class DatabaseConfigurationProvider : ConfigurationProvider, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly bool _reloadOnChange;
        private readonly TimeSpan _reloadDelay;
        private readonly Expression<Func<Configuration, bool>>? _predicate = null;
        private readonly DatabaseWatcher _watcher;
        private readonly IDisposable _change;

        public DatabaseConfigurationProvider(IServiceProvider serviceProvider, bool reloadOnChange, TimeSpan reloadDelay, Expression<Func<Configuration, bool>>? predicate)
        {
            _serviceProvider = serviceProvider;
            _reloadOnChange = reloadOnChange;
            _reloadDelay = reloadDelay;
            _predicate = predicate;

            if (_reloadOnChange)
            {
                _watcher = new DatabaseWatcher(reloadDelay);
                _change = ChangeToken.OnChange(_watcher.Watch, Load);
                EntityChangeObserver.Instance.ChangedConfiguration += EntityChangeObserver_ChangedConfiguration;
            }
        }

        public override void Load()
        {
            Data.Clear();
            Data = Configuration.GetDictionary(_predicate);

            OnReload();
        }

        protected IConfigurationReadRepository Configuration => _serviceProvider.GetRequiredService<IConfigurationReadRepository>();

        private void EntityChangeObserver_ChangedConfiguration(object? sender, Type e)
        {
            if (e != typeof(Configuration))
                return;

            Thread.Sleep(_reloadDelay.Milliseconds);
            Load();
        }

        public void Dispose()
        {
            _watcher?.Dispose();
            _change?.Dispose();
        }
    }
}
