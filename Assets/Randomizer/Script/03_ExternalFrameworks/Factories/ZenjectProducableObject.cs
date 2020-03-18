using System;
using Randomizer.ExternalFrameworks.Views;
using Zenject;

namespace Randomizer.ExternalFrameworks
{
    public class ZenjectProducableObject : BaseView, IPoolable<IMemoryPool>, IDisposable
    {
        private IMemoryPool _pool;

        public virtual void Dispose()
        {
            _pool?.Despawn(this);
        }

        public virtual void OnDespawned()
        {
            _pool = null;
        }

        public virtual void OnSpawned(IMemoryPool pool)
        {
            _pool = pool;
        }
    }
}