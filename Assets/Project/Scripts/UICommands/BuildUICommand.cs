namespace bts {

  public class BuildUICommand : UICommand {
    UnitCommander Commander { get; }
    PlacedObjectType BuildingType { get; }

    public BuildUICommand(BuildUICommandData data, UnitCommander commander)
      : base(data.ButtonIcon, data.TooltipData) {
      Commander = commander;
      BuildingType = data.BuildingType;
    }

    public override void Execute() {
      Commander.SetBuildingToBuild(BuildingType);
    }
  }
}
