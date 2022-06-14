using fro.BuildingSystem;
using UnityEngine;

namespace bts {
  public class UnitBuildCommand : Command {
    Unit Unit { get; }
    PlacedObjectData BuildingType { get; }
    CustomBuildingData CustomBuildingData { get; }
    Vector3 PositionToBuild { get; }

    public UnitBuildCommand(Unit unit, PlacedObjectData buildingType, CustomBuildingData customBuildingData, Vector3 positionToBuild) {
      Unit = unit;
      BuildingType = buildingType;
      CustomBuildingData = customBuildingData;
      PositionToBuild = positionToBuild;
    }

    public void Execute() {
      if (CustomBuildingData.CanPlace()) {
        Unit.IsOrderedToBuild = true;
        Unit.BuildingToPlace = BuildingType;
        Unit.CustomBuildingData = CustomBuildingData;
        Unit.Destination = PositionToBuild;
      }
    }
  }
}
