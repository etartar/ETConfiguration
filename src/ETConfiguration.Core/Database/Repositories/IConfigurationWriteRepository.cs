using ETConfiguration.Core.Database.Entities;

namespace ETConfiguration.Core.Database.Repositories
{
    public interface IConfigurationWriteRepository
    {
        Task<Configuration> AddAsync(Configuration entity);
        Configuration Add(Configuration entity);
        Configuration Update(Configuration entity);
        void Delete(Configuration entity);
    }
}