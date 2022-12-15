using ETConfiguration.Core.Database.Entities;
using ETConfiguration.Core.Database.Repositories;
using System.Linq.Expressions;

namespace ETConfiguration.Core.Database.Providers
{
    public class DatabaseConfigurationProvider : BaseDatabaseProvider
    {
        public DatabaseConfigurationProvider(IReadConfigurationRepository repository, bool reloadOnChange, int reloadDelay, Expression<Func<Configuration, bool>>? predicate) 
            : base(repository, reloadOnChange, reloadDelay, predicate)
        {
        }

        public override void Load()
        {
            Data = _repository.GetDictionary(_predicate);
        }
    }
}
