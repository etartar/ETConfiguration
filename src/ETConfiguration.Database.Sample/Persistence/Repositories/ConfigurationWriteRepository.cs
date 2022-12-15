using ETConfiguration.Core.Database.Entities;
using ETConfiguration.Core.Database.Repositories;
using ETConfiguration.Database.Sample.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ETConfiguration.Database.Sample.Persistence.Repositories
{
    public class ConfigurationWriteRepository : ConfigurationRepositoryBase, IConfigurationWriteRepository
    {
        public ConfigurationWriteRepository(ConfigurationContext context) : base(context)
        {
        }

        public Configuration Add(Configuration entity)
        {
            Context.Entry(entity).State = EntityState.Added;
            Complete();
            return entity;
        }

        public async Task<Configuration> AddAsync(Configuration entity)
        {
            Context.Entry(entity).State = EntityState.Added;
            await CompleteAsync();
            return entity;
        }

        public Configuration Update(Configuration entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Complete();
            return entity;
        }

        public void Delete(Configuration entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            Complete();
        }
    }
}
