using UnityEngine;

namespace bts {
  public class GridUpdater : MonoBehaviour {
    [SerializeField] Collider objectCollider;
    
    public void UpdateGraph() {
      AstarPath.active?.UpdateGraphs(objectCollider.bounds);
    }
  }
}
