namespace bts {
  public class CancelBuildUICommand : UICommand {
    UnitCommander Commander { get; }

    public CancelBuildUICommand(CancelBuildUICommandData data, UnitCommander commander)
      : base(data) {
      Commander = commander;
    }

    public override void Execute() {
      Commander.ClearBuildingToBuild();
    }
  }
}
