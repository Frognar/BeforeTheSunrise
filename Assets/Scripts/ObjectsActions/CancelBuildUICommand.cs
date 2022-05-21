using UnityEngine;

namespace bts {
  public class CancelBuildUICommand : UICommand {
    UnitCommander Commander { get; }

    public CancelBuildUICommand(CancelBuildUICommandData data, UnitCommander commander)
      : base(data.ButtonIcon, data.TooltipData) {
      Commander = commander;
    }

    public override void Execute() {
      Commander.ClearBuildingToBuild();
    }
  }
}
