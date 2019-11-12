using System;
using UnityEngine;
using Zenject;

namespace Randomizer.ExternalFrameworks
{
    public class ZenjectProducableObject : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable
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