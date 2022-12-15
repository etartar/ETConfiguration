using ETConfiguration.Core.Database.Entities;

namespace ETConfiguration.Core.Database.Repositories
{
    public interface IWriteConfigurationRepository
    {
        Task<Configuration> AddAsync(Configuration entity);
        Configuration Add(Configuration entity);
        Task<Configuration> UpdateAsync(Configuration entity);
        Configuration Update(Configuration entity);
        Task<Configuration> DeleteAsync(Configuration entity);
        Configuration Delete(Configuration entity);
    }
}