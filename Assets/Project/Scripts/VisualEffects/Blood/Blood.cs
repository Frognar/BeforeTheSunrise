using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.VFX;

namespace bts {
  public class Blood : MonoBehaviour {
    [SerializeField] VisualEffect visualEffect;

    public IObjectPool<Blood> pool;
    
    public void SetUp(Vector3 position, BloodConfiguration config) {
      transform.position = position;
      visualEffect.SetVector4("Color", config.Color);
      Invoke(nameof(Release), 10);
    }

    void Release() {
      pool.Release(this);
    }
  }
}
