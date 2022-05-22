using UnityEngine;

namespace bts {
  public class UnitBuildCommand : Command {
    Unit Unit { get; }
    PlacedObjectTypeSO BuildingType { get; }
    Vector3 PositionToBuild { get; }

    public UnitBuildCommand(Unit unit, PlacedObjectTypeSO buildingType, Vector3 positionToBuild) {
      Unit = unit;
      BuildingType = buildingType;
      PositionToBuild = positionToBuild;
    }

    public void Execute() {
      Unit.IsOrderedToBuild = true;
      Unit.BuildingToPlace = BuildingType;
      Unit.Destination = PositionToBuild;
    }
  }
}
