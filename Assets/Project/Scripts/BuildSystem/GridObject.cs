using fro.BuildingSystem;

namespace bts {
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
      Grid.TriggerOnGridObjectChanged(new GridCords(X, Z));
    }

    public void ClearPlacedObject() {
      SetPlacedObject(null);
    }

    public bool CanBuild() {
      return PlacedObject == null && !IsBlocked;
    }
  }
}
