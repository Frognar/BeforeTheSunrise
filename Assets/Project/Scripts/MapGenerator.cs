using System.Collections;
using System.Collections.Generic;
using fro.BuildingSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace fro.bts {
  public class MapGenerator : MonoBehaviour {
    public static MapGenerator instance;

    [SerializeField] private PlacedObjectData obstaclePrefab;
    [SerializeField] private List<PlacedObjectData> resourcesPrefabs;
    [SerializeField] private PlacedObjectData spawner;
    [SerializeField] private GridBuildingSystem gridBuildingSystem;

    public float Progress { get; private set; }
    public bool IsDone { get; private set; }

    private void Awake() {
      instance = this;
    }

    private IEnumerator Start() {
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

          if (IsInMapCenterArea(x, z, grid.Height, grid.Width)) {
            continue;
          }

          GenerateObject(DetermineGenerationType(), x, z);
        }
      }

      yield return null;
      GenerateMapCenterSpawner(grid.Width, grid.Height);
      IsDone = true;
    }
    
    private static bool IsInMapCenterArea(int x, int z, int mapHeight, int mapWidth) {
      const int centerMargin = 16;
      int middleHeight = mapHeight / 2;
      int middleWidth = mapWidth / 2;
      return z <= middleHeight + centerMargin
             && z >= middleHeight - (centerMargin + 1)
             && x <= middleWidth + centerMargin
             && x >= middleWidth - (centerMargin + 1);
    }

    private void GenerateObject(GenerationType generationType, int x, int z) {
      switch (generationType)
      {
        case GenerationType.Resource:
          gridBuildingSystem.Build(new GridCords(x, z), resourcesPrefabs[Random.Range(0, resourcesPrefabs.Count)]);
          break;
        case GenerationType.Obstacle:
          gridBuildingSystem.Build(new GridCords(x, z), obstaclePrefab);
          break;
        case GenerationType.Spawner:
          gridBuildingSystem.Build(new GridCords(x, z), spawner);
          break;
      }
    }

    private void GenerateMapCenterSpawner(int mapWidth, int mapHeight) {
      int centerX = (mapWidth - spawner.Width) / 2;
      int centerZ = (mapHeight - spawner.Height) / 2;
      GenerateObject(GenerationType.Spawner, centerX, centerZ);
    }
    
    private static GenerationType DetermineGenerationType() {
      return Random.value switch {
        > .95f => GenerationType.Resource,
        > .35f => GenerationType.Obstacle,
        _ => GenerationType.Nothing,
      };
    }

    private enum GenerationType {
      Nothing,
      Obstacle,
      Resource,
      Spawner,
    }
  }
}
