using ETConfiguration.Core.Database.Entities;
using ETConfiguration.Core.Database.Observers;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ETConfiguration.Database.Sample.Persistence.Contexts
{
    public class ConfigurationContext : DbContext
    {
        public ConfigurationContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Configuration> Configurations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override int SaveChanges()
        {
            OnEntityChange();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnEntityChange();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void OnEntityChange()
        {
            foreach (var entry in ChangeTracker.Entries<Configuration>())
            {
                if (entry.State == EntityState.Added ||
                    entry.State == EntityState.Modified ||
                    entry.State == EntityState.Deleted)
                {
                    EntityChangeObserver.Instance.OnChangedConfiguration(entry.Entity.GetType());
                }
            }
        }
    }
}
