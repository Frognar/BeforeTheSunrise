using UnityEngine;

namespace bts {
  public class UpgradeBuildingUICommand : UICommand {
    Building Building { get; }
    UpgradeBuildingUICommandData Data { get; }
    GemstoneDictionary Cost { get; }

    TooltipData tooltipData;
    public override TooltipData TooltipData => tooltipData;
    
    public UpgradeBuildingUICommand(Building building, UpgradeBuildingUICommandData commandData) : base(commandData) {
      Building = building;
      Data = commandData;
      Cost = Data.baseCost * (int)Mathf.Pow(2, Building.BuildingLevel);
      tooltipData = new TooltipData(
        Data.TooltipData.Header,
        Data.TooltipData.Content,
        Cost
      );
    }

    public override bool CanExecute() {
      return Building.IsCurrentMaxLevel() == false;
    }

    public override void Execute() {
      if (Building.CanUpgrade(Cost)) {
        Building.Upgrgade(Cost);
      }
    }
  }
}
