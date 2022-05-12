using UnityEngine;

namespace bts {
  public class UnitBuildCommand : Command {
    UnitStateManager Unit { get; }
    PlacedObjectTypeSO BuildingType { get; }
    Vector3 PositionToBuild { get; }

    public UnitBuildCommand(UnitStateManager unit, PlacedObjectTypeSO buildingType, Vector3 positionToBuild) {
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
