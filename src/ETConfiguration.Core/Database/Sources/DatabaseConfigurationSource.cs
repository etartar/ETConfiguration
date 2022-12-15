using ETConfiguration.Core.Database.Entities;
using ETConfiguration.Core.Database.Providers;
using ETConfiguration.Core.Database.Repositories;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

namespace ETConfiguration.Core.Database.Sources
{
    public class DatabaseConfigurationSource : IConfigurationSource
    {
        private readonly IReadConfigurationRepository _repository;
        private readonly bool _reloadOnChange;
        private readonly int _reloadDelay;
        private readonly Expression<Func<Configuration, bool>>? _predicate = null;
        private readonly bool _isAsyncProvider = false;

        public DatabaseConfigurationSource(IReadConfigurationRepository repository, bool reloadOnChange, int reloadDelay, Expression<Func<Configuration, bool>>? predicate = null, bool isAsyncProvider = false)
        {
            _repository = repository;
            _reloadOnChange = reloadOnChange;
            _reloadDelay = reloadDelay;
            _predicate = predicate;
            _isAsyncProvider = isAsyncProvider;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return _isAsyncProvider 
                ? new DatabaseConfigurationProviderAsync(_repository, _reloadOnChange, _reloadDelay, _predicate) 
                : new DatabaseConfigurationProvider(_repository, _reloadOnChange, _reloadDelay, _predicate);
        }
    }
}
