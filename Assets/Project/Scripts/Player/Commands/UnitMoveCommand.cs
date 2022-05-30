using UnityEngine;

namespace bts {
  public class UnitMoveCommand : Command {
    Unit Unit { get; }
    Vector3 Destination { get; }

    public UnitMoveCommand(Unit unit, Vector3 destination) {
      Unit = unit;
      Destination = destination;
    }

    public void Execute() {
      Unit.IsOrderedToMove = true;
      Unit.Destination = Destination;
    }
  }
}
