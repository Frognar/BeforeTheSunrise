using System.Collections.Generic;
using fro.BuildingSystem;
using UnityEngine;

namespace fro.bts {
  public class MapGenerator : MonoBehaviour {
    [SerializeField] PlacedObjectData obstaclePrefab;
    [SerializeField] List<PlacedObjectData> resourcesPrefabs;
    GridBuildingSystem gridBuildingSystem;

    PlacedObjectData RandomResource => resourcesPrefabs[Random.Range(0, resourcesPrefabs.Count)];

    void Awake() {
      gridBuildingSystem = FindObjectOfType<GridBuildingSystem>();  
    }

    void Start() {
      GridXZ<GridPlacedObject> grid = gridBuildingSystem.Grid;

      for (int x = 0; x < grid.Width; x++) {
        for (int z = 0; z < grid.Height; z++) {
          if (z <= grid.Height / 2 + 15
           && z >= grid.Height / 2 - 16
           && x <= grid.Width / 2 + 15
           && x >= grid.Width / 2 - 16) {
            continue;
          }

          if (Random.value <= .85f) {
            gridBuildingSystem.Build(new GridCords(x, z), Random.value > .95 ? RandomResource : obstaclePrefab);
          }
        }
      }
    }
  }
}
