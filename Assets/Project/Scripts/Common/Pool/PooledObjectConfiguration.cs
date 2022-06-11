using UnityEngine;

namespace fro.Pools {
  public abstract class PooledObjectConfiguration<T> : ScriptableObject
    where T : MonoBehaviour, Poolable {
    public abstract void ApplyTo(T obj);
  }
}
