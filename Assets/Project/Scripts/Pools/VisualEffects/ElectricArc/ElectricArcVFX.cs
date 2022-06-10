using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.VFX;

namespace bts {
  public class ElectricArcVFX : MonoBehaviour, Poolable {
    [field: SerializeField] public CustomVFXTransformBinder SourceTransformBinder { get; private set; }
    [field: SerializeField] public VisualEffect VisualEffect { get; private set; }
    IObjectPool<ElectricArcVFX> electricArcPool;

    public void ReleaseAfter(float duration) {
      Invoke(nameof(Release), duration);
    }

    void Release() {
      electricArcPool.Release(this);
    }

    public void SetPool<T>(IObjectPool<T> pool) where T : MonoBehaviour, Poolable {
      electricArcPool = (IObjectPool<ElectricArcVFX>)pool;
    }
  }
}
