using fro.BuildingSystem;

namespace bts {

  public class BuildUICommand : UICommand {
    public CustomBuildingData BuildingData { get; }
    UnitCommander Commander { get; }
    PlacedObjectData BuildingType { get; }

    public BuildUICommand(BuildUICommandData data, UnitCommander commander)
      : base(data) {
      Commander = commander;
      BuildingData = data.CustomBuildingData;
      BuildingType = data.BuildingType;
    }

    public override void Execute() {
      if (BuildingData.CanPlace()) {
        Commander.SetBuildingToBuild(BuildingType, BuildingData);
      }
    }
  }
}
