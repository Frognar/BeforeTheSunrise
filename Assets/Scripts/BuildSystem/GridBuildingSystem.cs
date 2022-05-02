using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace bts {
  public class GridBuildingSystem : MonoBehaviour {
    [SerializeField][Range(1, 1000)] int gridWidth;
    [SerializeField][Range(1, 1000)] int gridHeight;
    [SerializeField][Range(1f, 50f)] float cellSize;
    [SerializeField] Vector3 gridOrigin;
    [SerializeField] PlacedObjectTypeSO buildingSO;

    GridXZ<GridObject> grid;
    PlayerInputs playerInputs;

    void Awake() {
      playerInputs = FindObjectOfType<PlayerInputs>();
      grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, gridOrigin, (g, x, z) => new GridObject(g, x, z));
    }

    void Update() {
      if (playerInputs.IsLeftBtnDawn) {
        Vector3Int cords = grid.GetCords(playerInputs.GetMouseWorldPosition());
        List<Vector3Int> gridPositions = buildingSO.GetGridPositions(cords);
        List<GridObject> gridObjects = gridPositions.ConvertAll(p => grid.GetGridObject(p.x, p.z));
        if (gridObjects.All(o => o.CanBuild())) {
          PlacedObject placedObject = PlacedObject.Create(grid.GetWorldPosition(cords), cords, buildingSO);
          gridObjects.ForEach(o => o.SetPlacedObject(placedObject));
        }
      }

      if (playerInputs.IsRightBtnDawn) {
        GridObject gridObject = grid.GetGridObject(playerInputs.GetMouseWorldPosition());
        PlacedObject placedObject = gridObject.PlacedObject;
        if (placedObject != null) {
          List<Vector3Int> gridPositions = placedObject.GetGridPositions();
          List<GridObject> gridObjects = gridPositions.ConvertAll(p => grid.GetGridObject(p.x, p.z));
          gridObjects.ForEach(o => o.ClearPlacedObject());
          placedObject.DestroySelf();
        }
      }
    }

    public class GridObject {
      int X { get; }
      int Z { get; }
      GridXZ<GridObject> Grid { get; }
      public PlacedObject PlacedObject { get; private set; }

      public GridObject(GridXZ<GridObject> grid, int x, int y) {
        Grid = grid;
        X = x;
        Z = y;
      }

      public void SetPlacedObject(PlacedObject placedObject) {
        PlacedObject = placedObject;
        Grid.TriggerOnGridObjectChanged(X, Z);
      }

      public void ClearPlacedObject() {
        SetPlacedObject(null);
      }

      public bool CanBuild() {
        return PlacedObject == null;
      }
    }
  }
}
