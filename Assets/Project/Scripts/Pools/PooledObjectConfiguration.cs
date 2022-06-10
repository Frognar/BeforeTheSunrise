using UnityEngine;

namespace bts {
  public abstract class PooledObjectConfiguration<T> : ScriptableObject
    where T : MonoBehaviour, Poolable {
    public abstract void ApplyTo(T obj);
  }
}
