using ETConfiguration.Core.Database.Entities;
using System.Linq.Expressions;

namespace ETConfiguration.Core.Database.Providers
{
    public class DatabaseConfigurationProvider : DatabaseProviderBase
    {
        public DatabaseConfigurationProvider(IServiceProvider serviceProvider, bool reloadOnChange, int reloadDelay, Expression<Func<Configuration, bool>>? predicate) 
            : base(serviceProvider, reloadOnChange, reloadDelay, predicate)
        {
        }

        public override void Load()
        {
            Data.Clear();
            Data = Configuration.GetDictionary(_predicate);

            OnReload();
        }
    }
}
