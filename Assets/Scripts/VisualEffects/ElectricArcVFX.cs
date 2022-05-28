using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.VFX;

namespace bts {
  public class ElectricArcVFX : MonoBehaviour {
    [SerializeField] CustomVFXTransformBinder sourceTransformBinder;
    [SerializeField] CustomVFXTransformBinder targetTransformBinder;
    [SerializeField] VisualEffect visualEffect;
    public IObjectPool<ElectricArcVFX> pool;

    public void SetUp(Transform sourceTransform, Transform targetTransform, Vector3 color, float duration = 0.5f) {
      sourceTransformBinder.Target = sourceTransform;
      targetTransformBinder.Target = targetTransform;
      visualEffect.SetVector3("ArcColor", color);
      Invoke(nameof(Release), duration);
    }
    
    void Release() {
      pool.Release(this);
    }
  }
}
