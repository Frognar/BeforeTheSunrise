using UnityEngine;
using UnityEngine.Pool;

namespace bts {
  public interface Poolable {
    public void SetPool<T>(IObjectPool<T> pool) where T : MonoBehaviour, Poolable;
  }
}
