using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace bts {
  public class GridBuildingSystem : MonoBehaviour {
    public GridXZ<GridObject> Grid { get; private set; }
    [SerializeField] IntAsset gridWidth;
    [SerializeField] IntAsset gridHeight;
    [SerializeField][Range(1f, 50f)] float cellSize;
    Vector3 gridOrigin;
    PlaceableFactory placeableFactory;

    void Awake() {
      placeableFactory = GetComponent<PlaceableFactory>();
      gridOrigin = new Vector3(-gridWidth * cellSize / 2f, 0f, -gridHeight * cellSize / 2f);
      Grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, gridOrigin, (g, x, z) => new GridObject(g, x, z));
    }

    public void Build(Vector3 mouseWorldPosition, PlacedObjectTypeSO placedObjectType) {
      Vector3Int cords = Grid.GetCords(mouseWorldPosition);
      Build(cords, placedObjectType);
    }

    public void Build(Vector3Int cords, PlacedObjectTypeSO placedObjectType) {
      List<Vector3Int> gridPositions = placedObjectType.GetGridPositions(cords);
      List<GridObject> gridObjects = gridPositions.ConvertAll(p => Grid.GetGridObject(p.x, p.z));
      if (gridObjects.All(o => o?.CanBuild() ?? false)) {
        Placeable placedObject = placeableFactory.Create(Grid.GetWorldPosition(cords), cords, placedObjectType, this);
        gridObjects.ForEach(o => o.SetPlacedObject(placedObject));
      }
    }

    public void Block(List<Vector3Int> cords) {
      cords.ForEach(p => Grid.GetGridObject(p.x, p.z).Block());
    }

    public void Demolish(Vector3 mouseWorldPosition) {
      GridObject gridObject = Grid.GetGridObject(mouseWorldPosition);
      Placeable placedObject = gridObject.PlacedObject;
      if (placedObject != null) {
        List<Vector3Int> gridPositions = placedObject.GetGridPositions();
        List<GridObject> gridObjects = gridPositions.ConvertAll(p => Grid.GetGridObject(p.x, p.z));
        gridObjects.ForEach(o => o.ClearPlacedObject());
        placedObject.Demolish();
      }
    }

    public class GridObject {
      int X { get; }
      int Z { get; }
      GridXZ<GridObject> Grid { get; }
      public Placeable PlacedObject { get; private set; }
      public bool IsBlocked { get; private set; }

      public GridObject(GridXZ<GridObject> grid, int x, int y) {
        Grid = grid;
        X = x;
        Z = y;
      }
      
      public void Block() {
        IsBlocked = true;
      }

      public void SetPlacedObject(Placeable placedObject) {
        PlacedObject = placedObject;
        Grid.TriggerOnGridObjectChanged(X, Z);
      }

      public void ClearPlacedObject() {
        SetPlacedObject(null);
      }

      public bool CanBuild() {
        return PlacedObject == null && !IsBlocked;
      }
    }
  }
}
