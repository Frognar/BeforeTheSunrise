using fro.Pools;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.VFX;

namespace bts {
  public class BloodVFX : MonoBehaviour, Poolable {
    [field: SerializeField] public VisualEffect VisualEffect { get; private set; }
    IObjectPool<BloodVFX> bloodPool;
    
    public void ReleaseAfter(float duration) {
      Invoke(nameof(Release), duration);
    }

    void Release() {
      bloodPool.Release(this);
    }

    public void SetPool<T>(IObjectPool<T> pool) where T : MonoBehaviour, Poolable {
      bloodPool = (IObjectPool<BloodVFX>)pool;
    }
  }
}
