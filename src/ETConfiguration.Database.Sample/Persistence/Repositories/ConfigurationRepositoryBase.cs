using ETConfiguration.Core.Database.Entities;
using ETConfiguration.Database.Sample.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ETConfiguration.Database.Sample.Persistence.Repositories
{
    public class ConfigurationRepositoryBase
    {
        private readonly ConfigurationContext _context;

        public ConfigurationRepositoryBase(ConfigurationContext context)
        {
            _context = context;
        }

        protected DbSet<Configuration> Table => _context.Set<Configuration>();

        protected ConfigurationContext Context => _context;

        protected void Complete() => _context.SaveChanges();

        protected async Task CompleteAsync() => await _context.SaveChangesAsync();
    }
}
