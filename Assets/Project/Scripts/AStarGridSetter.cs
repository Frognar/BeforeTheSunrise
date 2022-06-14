using fro.ValueAssets;
using Pathfinding;
using UnityEngine;

namespace bts {
  public class AStarGridSetter : MonoBehaviour {
    [SerializeField] IntAsset mapWidth;
    [SerializeField] IntAsset mapHeight;
    
    void Awake() {
      AstarData data = AstarPath.active.data;
      GridGraph gg = data.graphs[0] as GridGraph;
      float cellSize = .5f;
      gg.SetDimensions(Mathf.CeilToInt(mapWidth / cellSize), Mathf.CeilToInt(mapHeight / cellSize), cellSize);
      AstarPath.active.Scan();
    }
  }
}
