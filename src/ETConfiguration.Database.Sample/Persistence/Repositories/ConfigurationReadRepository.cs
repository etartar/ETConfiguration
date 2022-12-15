using ETConfiguration.Core.Database.Entities;
using ETConfiguration.Core.Database.Repositories;
using ETConfiguration.Database.Sample.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ETConfiguration.Database.Sample.Persistence.Repositories
{
    public class ConfigurationReadRepository : ConfigurationRepositoryBase, IConfigurationReadRepository
    {
        public ConfigurationReadRepository(ConfigurationContext context) : base(context)
        {
        }

        public Configuration Get(Expression<Func<Configuration, bool>>? predicate = null)
        {
            return Table.Where(predicate).FirstOrDefault();
        }

        public async Task<Configuration> GetAsync(Expression<Func<Configuration, bool>>? predicate = null)
        {
            return await Table.Where(predicate).FirstOrDefaultAsync();
        }

        public Dictionary<string, string> GetDictionary(Expression<Func<Configuration, bool>>? predicate = null)
        {
            return Table.AsNoTracking().Where(predicate).ToDictionary(c => $"{c.Section}:{c.Key}", c => c.Value);
        }

        public async Task<Dictionary<string, string>> GetDictionaryAsync(Expression<Func<Configuration, bool>>? predicate = null, CancellationToken cancellationToken = default)
        {
            return await Table.AsNoTracking().Where(predicate).ToDictionaryAsync(c => $"{c.Section}:{c.Key}", c => c.Value);
        }

        public List<Configuration> GetList(Expression<Func<Configuration, bool>>? predicate = null)
        {
            return Table.Where(predicate).ToList();
        }

        public async Task<List<Configuration>> GetListAsync(Expression<Func<Configuration, bool>>? predicate = null, CancellationToken cancellationToken = default)
        {
            return await Table.Where(predicate).ToListAsync();
        }
    }
}
