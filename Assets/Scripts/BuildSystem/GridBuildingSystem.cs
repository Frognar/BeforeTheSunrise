using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace bts {
  public class GridBuildingSystem : MonoBehaviour {
    public GridXZ<GridObject> Grid { get; private set; }
    [SerializeField][Range(1, 1000)] int gridWidth;
    [SerializeField][Range(1, 1000)] int gridHeight;
    [SerializeField][Range(1f, 50f)] float cellSize;
    [SerializeField] Vector3 gridOrigin;
    [SerializeField] PlacedObjectTypeSO buildingSO;
    GhostObject currentGhost;
    PlayerInputs playerInputs;

    void Awake() {
      Grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, gridOrigin, (g, x, z) => new GridObject(g, x, z));
      playerInputs = FindObjectOfType<PlayerInputs>();
    }

    void Start() {
      currentGhost = Instantiate(buildingSO.ghost).GetComponent<GhostObject>();
    }

    void Update() {
      if (playerInputs.IsLeftBtnDawn) {
        Build(playerInputs.GetMouseWorldPosition(), buildingSO);
      }
    }

    public void Build(Vector3 mouseWorldPosition, PlacedObjectTypeSO placedObjectType) {
      Vector3Int cords = Grid.GetCords(mouseWorldPosition);
      Build(cords, placedObjectType);
    }

    public void Build(Vector3Int cords, PlacedObjectTypeSO placedObjectType) {
      List<Vector3Int> gridPositions = buildingSO.GetGridPositions(cords);
      List<GridObject> gridObjects = gridPositions.ConvertAll(p => Grid.GetGridObject(p.x, p.z));
      if (gridObjects.All(o => o?.CanBuild() ?? false)) {
        PlacedObject placedObject = PlacedObject.Create(Grid.GetWorldPosition(cords), cords, placedObjectType, this);
        gridObjects.ForEach(o => o.SetPlacedObject(placedObject));
      }
    }

    public void Demolish(Vector3 mouseWorldPosition) {
      GridObject gridObject = Grid.GetGridObject(mouseWorldPosition);
      PlacedObject placedObject = gridObject.PlacedObject;
      if (placedObject != null) {
        List<Vector3Int> gridPositions = placedObject.GetGridPositions();
        List<GridObject> gridObjects = gridPositions.ConvertAll(p => Grid.GetGridObject(p.x, p.z));
        gridObjects.ForEach(o => o.ClearPlacedObject());
        placedObject.DestroySelf();
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
