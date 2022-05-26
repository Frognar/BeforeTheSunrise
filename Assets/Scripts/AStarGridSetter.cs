using Pathfinding;
using UnityEngine;

namespace bts {
  public class AStarGridSetter : MonoBehaviour {
    [SerializeField] IntAsset mapWidth;
    [SerializeField] IntAsset mapHeight;
    
    void Awake() {
      AstarData data = AstarPath.active.data;
      GridGraph gg = data.graphs[0] as GridGraph;
      gg.SetDimensions(mapWidth, mapHeight, .5f);
      AstarPath.active.Scan();
    }
  }
}
