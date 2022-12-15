using ETConfiguration.Core.Database.Entities;
using ETConfiguration.Core.Database.Observers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ETConfiguration.Core.Database.Interceptors
{
    public class EntityChangeObserverInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        private void OnEntityChange(DbContext? context)
        {
            if (context is null) return;

            foreach (var entry in context.ChangeTracker.Entries<Configuration>())
            {
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    EntityChangeObserver.Instance.OnChangedConfiguration(entry.Entity.GetType());
                }
            }
        }
    }
}
