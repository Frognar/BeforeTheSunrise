using UnityEngine;
using UnityEngine.Pool;

namespace bts {
  public class Laser : MonoBehaviour {
    [SerializeField] LineRenderer lineRenderer;
    public IObjectPool<Laser> pool;
    Transform target;
    Vector3 lastTargetPosition;
    
    public void SetUp(Vector3 sourcePosition, Transform target, LaserConfiguration config) {
      transform.position = sourcePosition;
      lineRenderer.SetPositions(new Vector3[] { sourcePosition, target.position });
      lineRenderer.material = config.Material;
      lastTargetPosition = target.position;
    }

    void Update() {
      if (target != null && target.position != lastTargetPosition) {
        lastTargetPosition = target.position;
        lineRenderer.SetPosition(1, target.position);
      }
    }

    public void Release() {
      pool.Release(this);
    }
  }
}
