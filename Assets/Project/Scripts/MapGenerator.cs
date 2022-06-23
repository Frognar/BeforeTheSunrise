using System.Collections;
using System.Collections.Generic;
using fro.BuildingSystem;
using UnityEngine;

namespace fro.bts {
  public class MapGenerator : MonoBehaviour {
    public static MapGenerator instance;

    [SerializeField] PlacedObjectData obstaclePrefab;
    [SerializeField] List<PlacedObjectData> resourcesPrefabs;
    [SerializeField] PlacedObjectData spawner;
    [SerializeField] GridBuildingSystem gridBuildingSystem;
    public float Progress { get; private set; }
    public bool IsDone { get; private set; }

    PlacedObjectData RandomResource => resourcesPrefabs[Random.Range(0, resourcesPrefabs.Count)];

    void Awake() {
      instance = this;
    }

    IEnumerator Start() {
      GridXZ<GridPlacedObject> grid = gridBuildingSystem.Grid;

      int total = grid.Width * grid.Height;
      int current = 0;
      for (int x = 0; x < grid.Width; x++) {
        for (int z = 0; z < grid.Height; z++) {
          current++;
          Progress = (float)current / total;
          if (current % (total / 10) == 0) {
            yield return null;
          }

          if (z <= grid.Height / 2 + 16
           && z >= grid.Height / 2 - 17
           && x <= grid.Width / 2 + 16
           && x >= grid.Width / 2 - 17) {
            continue;
          }

          if (Random.value <= .65f) {
            if (Random.value > .95f) {
              gridBuildingSystem.Build(new GridCords(x, z), RandomResource);
            }
            else {
              gridBuildingSystem.Build(new GridCords(x, z), obstaclePrefab);
            }
          }
        }
      }

      yield return null;
      GridCords spawnerCords = new GridCords((grid.Width - spawner.Width) / 2, (grid.Height - spawner.Height) / 2);
      gridBuildingSystem.Build(spawnerCords, spawner); 
      IsDone = true;
    }
  }
}
