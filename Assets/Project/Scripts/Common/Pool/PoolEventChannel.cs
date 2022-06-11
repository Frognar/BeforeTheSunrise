using System;
using UnityEngine;

namespace fro.Pools {
  public abstract class PoolEventChannel<T> : ScriptableObject
    where T : MonoBehaviour, Poolable {
    public Action<PooledObjectConfiguration<T>, PooledObjectParameters<T>> OnSpawnRequest;
    public Func<PooledObjectConfiguration<T>, PooledObjectParameters<T>, T> OnGetRequest;

    public void RaiseSpawnEvent(PooledObjectConfiguration<T> config, PooledObjectParameters<T> parameters) {
      OnSpawnRequest?.Invoke(config, parameters);
    }

    public T RaiseGetEvent(PooledObjectConfiguration<T> config, PooledObjectParameters<T> parameters) {
      return OnGetRequest?.Invoke(config, parameters);
    }
  }
}
