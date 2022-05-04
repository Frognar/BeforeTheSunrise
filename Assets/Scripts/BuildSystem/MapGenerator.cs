using System.Collections.Generic;
using UnityEngine;

namespace bts {
  public class MapGenerator : MonoBehaviour {
    [SerializeField] List<PlacedObjectTypeSO> buildingBlocks = new List<PlacedObjectTypeSO>();
    GridBuildingSystem gridBuildingSystem;

    void Awake() {
      gridBuildingSystem = FindObjectOfType<GridBuildingSystem>();  
    }

    void Start() {
      GridXZ<GridBuildingSystem.GridObject> grid = gridBuildingSystem.Grid;

      for (int x = 0; x < grid.Width; x++) {
        for (int z = 0; z < grid.Height; z++) {
          if (z <= grid.Height / 2 + 5 
           && z >= grid.Height / 2 - 5
           && x <= grid.Width / 2 + 5
           && x >= grid.Width / 2 - 5) {
            continue;
          }

          if (Random.value <= .85f) {
            gridBuildingSystem.Build(new Vector3Int(x, 0, z), buildingBlocks[0]);
          }
        }
      }
    }
  }
}
