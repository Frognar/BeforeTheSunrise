using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.VFX;

namespace bts {
  public class ElectricArcVFX : MonoBehaviour {
    [SerializeField] CustomVFXTransformBinder sourceTransformBinder;
    [SerializeField] CustomVFXTransformBinder targetTransformBinder;
    [SerializeField] VisualEffect visualEffect;
    public IObjectPool<ElectricArcVFX> pool;

    public void SetUp(Transform sourceTransform, Vector3 targetPosition, ElectricArcVFXConfiguration config) {
      sourceTransformBinder.Target = sourceTransform;
      visualEffect.SetVector3("TargetPosition", targetPosition);
      config.ApplyTo(visualEffect);
      Invoke(nameof(Release), config.Duration);
    }
    
    void Release() {
      pool.Release(this);
    }
  }
}
