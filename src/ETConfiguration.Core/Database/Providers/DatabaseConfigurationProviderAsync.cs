using ETConfiguration.Core.Database.Entities;
using ETConfiguration.Core.Database.Repositories;
using System.Linq.Expressions;

namespace ETConfiguration.Core.Database.Providers
{
    public class DatabaseConfigurationProviderAsync : BaseDatabaseProvider
    {
        public DatabaseConfigurationProviderAsync(IReadConfigurationRepository repository, bool reloadOnChange, int reloadDelay, Expression<Func<Configuration, bool>>? predicate) 
            : base(repository, reloadOnChange, reloadDelay, predicate)
        {
        }

        public override async void Load()
        {
            Data = await _repository.GetDictionaryAsync(_predicate);
        }
    }
}
