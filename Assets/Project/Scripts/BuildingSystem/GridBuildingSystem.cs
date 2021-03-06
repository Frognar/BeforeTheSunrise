using System.Collections.Generic;
using fro.ValueAssets;
using UnityEngine;

namespace fro.BuildingSystem {
  public class GridBuildingSystem : MonoBehaviour {
    public GridXZ<GridPlacedObject> Grid { get; private set; }
    [field: SerializeField] public IntAsset GridWidth { get; private set; }
    [field: SerializeField] public IntAsset GridHeight { get; private set; }
    [field: SerializeField][field: Range(1f, 50f)] public float CellSize { get; private set; } = 1f;
    [SerializeField] PlacedObjectFactory factory;
    Vector3 gridOrigin;

    void Awake() {
      float halfCellSize = CellSize * .5f;
      gridOrigin = new Vector3(-GridWidth * halfCellSize, 0f, -GridHeight * halfCellSize);
      Grid = new GridXZ<GridPlacedObject>(GridWidth, GridHeight, CellSize, gridOrigin, (grid, cords) => new GridPlacedObject(grid, cords));
    }

    public bool CanBuild(Vector3 worldPosition, PlacedObjectData objectData) {
      GridCords cords = Grid.GetCords(worldPosition);
      List<GridCords> gridPositions = objectData.GetGridPositions(cords);
      List<GridPlacedObject> gridPlacedObjects = GetGridPlacedObjects(gridPositions);
      return gridPlacedObjects.TrueForAll(o => o?.CanBuild() ?? false);
    }

    List<GridPlacedObject> GetGridPlacedObjects(List<GridCords> gridCords) {
      List<GridPlacedObject> gridPlacedObjects = new List<GridPlacedObject>();
      foreach (GridCords gridCord in gridCords) {
        gridPlacedObjects.Add(Grid.GetGridObject(gridCord));
      }
      
      return gridPlacedObjects;
    }

    public void Build(Vector3 worldPosition, PlacedObjectData objectData) {
      GridCords cords = Grid.GetCords(worldPosition);
      Build(cords, objectData);
    }

    public void Build(GridCords cords, PlacedObjectData objectData) {
      List<GridCords> gridPositions = objectData.GetGridPositions(cords);
      List<GridPlacedObject> gridPlacedObjects = GetGridPlacedObjects(gridPositions);
      if (gridPlacedObjects.TrueForAll(o => o?.CanBuild() ?? false)) {
        PlacedObject obj = factory.Create(cords, objectData, buildingSystem: this);
        gridPlacedObjects.ForEach(o => o.SetObject(obj));
      }
    }

    public void Demolish(Vector3 worldPosition) {
      GridPlacedObject gridPlacedObject = Grid.GetGridObject(worldPosition);
      PlacedObject placedObject = gridPlacedObject.PlacedObject;
      if (placedObject != null) {
        List<GridCords> gridPositions = placedObject.GetGridPositions();
        List<GridPlacedObject> gridPlacedObjects = GetGridPlacedObjects(gridPositions);
        gridPlacedObjects.ForEach(o => o.ClearPlacedObject());
        placedObject.Demolish();
      }
    }
  }
}
