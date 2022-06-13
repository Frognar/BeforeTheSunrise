using UnityEngine;

namespace bts {
  public class GridUpdater : MonoBehaviour {
    [SerializeField] Collider coll;
    
    public void UpdateGraph() {
      AstarPath.active?.UpdateGraphs(coll.bounds);
    }
  }
}
