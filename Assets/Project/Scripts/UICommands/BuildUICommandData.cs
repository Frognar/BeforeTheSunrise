using bts.Gemstones;
using fro.BuildingSystem;
using UnityEngine;

namespace bts {
  [CreateAssetMenu(fileName = "BuildCommandData", menuName = "UICommands/Build Command Data")]
  public class BuildUICommandData : UICommandData {
    [field: SerializeField] public PlacedObjectData BuildingType { get; private set; }
    [field: SerializeField] public CustomBuildingData CustomBuildingData { get; private set; }
    TooltipData tooltipData;
    public override TooltipData TooltipData => tooltipData;

    void OnEnable() {
      if (BuildingType != null) {
        string header = BuildingType.Name;
        string content = BuildingType.Description;
        if (CustomBuildingData is Limited limited) {
          content += $"\nCan placed only {limited.Limit} of those";
        }
        
        GemstoneDictionary gemstones = CustomBuildingData.buildingCosts;
        tooltipData = new TooltipData(header, content, gemstones);
      }
      else {
        tooltipData = new TooltipData(string.Empty, string.Empty, new GemstoneDictionary());
      }
    }
  }
}
