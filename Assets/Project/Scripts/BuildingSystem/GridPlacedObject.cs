namespace fro.BuildingSystem {
  public class GridPlacedObject {
    GridCords Cords { get; }
    GridXZ<GridPlacedObject> Grid { get; }
    public PlacedObject PlacedObject { get; private set; }

    public GridPlacedObject(GridXZ<GridPlacedObject> grid, GridCords cords) {
      Grid = grid;
      Cords = cords;
      PlacedObject = null;
    }

    public void SetObject(PlacedObject placedObject) {
      PlacedObject = placedObject;
      Grid.TriggerOnGridObjectChanged(Cords);
    }

    public void ClearPlacedObject() {
      SetObject(null);
    }

    public bool CanBuild() {
      return PlacedObject == null;
    }
  }
}
