using fro.Pools;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.VFX;

namespace bts {
  public class DestroyVFX : MonoBehaviour, Poolable {
    [field: SerializeField] public VisualEffect VisualEffect { get; private set; }
    IObjectPool<DestroyVFX> destroyPool;
    
    public void ReleaseAfter(float duration) {
      Invoke(nameof(Release), duration);
    }

    void Release() {
      destroyPool.Release(this);
    }
    
    public void SetPool<T>(IObjectPool<T> pool) where T : MonoBehaviour, Poolable {
      destroyPool = pool as IObjectPool<DestroyVFX>;
    }
  }
}
