using UnityEngine;

namespace bts {
  public abstract class PooledObjectParameters<T>
    where T : MonoBehaviour, Poolable {
    public abstract void SetTo(T obj);
  }
}
