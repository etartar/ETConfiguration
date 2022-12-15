using System.Linq.Expressions;
using ETConfiguration.Core.Database.Entities;

namespace ETConfiguration.Core.Database.Repositories
{
    public interface IConfigurationReadRepository
    {
        Task<Dictionary<string, string>> GetDictionaryAsync(Expression<Func<Configuration, bool>>? predicate = null, CancellationToken cancellationToken = default);
        Dictionary<string, string> GetDictionary(Expression<Func<Configuration, bool>>? predicate = null);
        Task<List<Configuration>> GetListAsync(Expression<Func<Configuration, bool>>? predicate = null, CancellationToken cancellationToken = default);
        List<Configuration> GetList(Expression<Func<Configuration, bool>>? predicate = null);
        Task<Configuration> GetAsync(Expression<Func<Configuration, bool>>? predicate = null);
        Configuration Get(Expression<Func<Configuration, bool>>? predicate = null);
    }
}