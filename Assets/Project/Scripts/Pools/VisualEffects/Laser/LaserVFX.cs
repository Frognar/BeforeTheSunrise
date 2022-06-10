using UnityEngine;
using UnityEngine.Pool;

namespace bts {
  public class LaserVFX : MonoBehaviour, Poolable {
    [field: SerializeField] public LineRenderer LineRenderer { get; private set; }
    public Transform Target {
      set {
        target = value;
        lastTargetPosition = target.position;
      }
    }
    
    Transform target;
    Vector3 lastTargetPosition;
    IObjectPool<LaserVFX> laserPool;

    void Update() {
      if (target != null && target.position != lastTargetPosition) {
        lastTargetPosition = target.position;
        LineRenderer.SetPosition(1, target.position);
      }
    }

    public void Release() {
      target = null;
      laserPool.Release(this);
    }

    public void SetPool<T>(IObjectPool<T> pool) where T : MonoBehaviour, Poolable {
      laserPool = (IObjectPool<LaserVFX>)pool;
    }
  }
}
