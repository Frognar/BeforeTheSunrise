using System.Collections.Generic;
using System.Linq;
using fro.BuildingSystem;
using UnityEngine;

namespace bts {
  public partial class GridBuildingSystem : MonoBehaviour {
    public GridXZ<GridObject> Grid { get; private set; }
    [SerializeField] IntAsset gridWidth;
    [SerializeField] IntAsset gridHeight;
    [SerializeField][Range(1f, 50f)] float cellSize;
    Vector3 gridOrigin;
    PlaceableFactory placeableFactory;

    void Awake() {
      placeableFactory = GetComponent<PlaceableFactory>();
      gridOrigin = new Vector3(-gridWidth * cellSize / 2f, 0f, -gridHeight * cellSize / 2f);
      Grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, gridOrigin, (g, c) => new GridObject(g, c.X, c.Z));
    }

    public bool CanBuild(Vector3 worldPosition, PlacedObjectType objectType) {
      GridCords cords = Grid.GetCords(worldPosition);
      return CanBuild(new Vector3Int(cords.X, 0, cords.Z), objectType);
    }

    public bool CanBuild(Vector3Int gridPosition, PlacedObjectType objectType) {
      List<GridObject> gridObjects = GetGriddObjectForBuilding(gridPosition, objectType);
      return CanBuild(gridObjects);
    }
    
    bool CanBuild(List<GridObject> gridObjects) {
      return gridObjects.All(o => o?.CanBuild() ?? false);
    }

    public void Build(Vector3 worldPosition, PlacedObjectType objectType) {
      GridCords cords = Grid.GetCords(worldPosition);
      Build(new Vector3Int(cords.X, 0, cords.Z), objectType);
    }

    List<GridObject> GetGriddObjectForBuilding(Vector3Int gridPosition, PlacedObjectType objectType) {
      List<Vector3Int> gridPositions = objectType.GetGridPositions(gridPosition);
      return gridPositions.ConvertAll(p => Grid.GetGridObject(new GridCords(p.x, p.z)));
    }

    public void Build(Vector3Int gridPosition, PlacedObjectType objectType) {
      List<GridObject> gridObjects = GetGriddObjectForBuilding(gridPosition, objectType);
      if (CanBuild(gridObjects)) {
        Placeable placedObject = placeableFactory.Create(Grid.GetWorldPosition(new GridCords(gridPosition.x, gridPosition.z)), gridPosition, objectType, this);
        gridObjects.ForEach(o => o.SetPlacedObject(placedObject));
      }
    }

    public void Block(List<Vector3Int> cords) {
      cords.ForEach(p => Grid.GetGridObject(new GridCords(p.x, p.z)).Block());
    }

    public void Demolish(Vector3 mouseWorldPosition) {
      GridObject gridObject = Grid.GetGridObject(mouseWorldPosition);
      Placeable placedObject = gridObject.PlacedObject;
      if (placedObject != null) {
        List<Vector3Int> gridPositions = placedObject.GetGridPositions();
        List<GridObject> gridObjects = gridPositions.ConvertAll(p => Grid.GetGridObject(new GridCords(p.x, p.z)));
        gridObjects.ForEach(o => o.ClearPlacedObject());
        placedObject.Demolish();
      }
    }
  }
}
