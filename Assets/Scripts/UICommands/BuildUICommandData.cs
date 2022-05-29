using UnityEngine;

namespace bts {
  [CreateAssetMenu(fileName = "BuildCommandData", menuName = "UICommands/Build Command Data")]
  public class BuildUICommandData : UICommandData {
    [field: SerializeField] public PlacedObjectType BuildingType { get; private set; }
    TooltipData tooltipData;
    public override TooltipData TooltipData => tooltipData;

    void OnEnable() {
      if (BuildingType != null) {
        string header = BuildingType.objectName;
        string content = BuildingType.objectDescription;
        GemstoneDictionary gemstones = (BuildingType.customData as CustomBuildingData).buildingCosts;
        tooltipData = new TooltipData(header, content, gemstones);
      }
      else {
        tooltipData = new TooltipData(string.Empty, string.Empty, new GemstoneDictionary());
      }
    }
  }
}
