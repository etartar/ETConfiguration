using ETConfiguration.Core.Database.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ETConfiguration.Core.Database.Observers
{
    public class EntityChangeObserver
    {
        public event EventHandler<Type> ChangedConfiguration;

        public virtual void OnChangedConfiguration(Type type)
        {
            ThreadPool.QueueUserWorkItem((_) => ChangedConfiguration?.Invoke(this, type));
        }

        private static readonly Lazy<EntityChangeObserver> lazy = new Lazy<EntityChangeObserver>(() => new EntityChangeObserver());

        private EntityChangeObserver() { }

        public static EntityChangeObserver Instance => lazy.Value;
    }
}
