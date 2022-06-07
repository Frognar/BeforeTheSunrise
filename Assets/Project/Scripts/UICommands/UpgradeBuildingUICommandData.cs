using UnityEngine;

namespace bts {
  [CreateAssetMenu(fileName = "Upgrgaded Building Command Data", menuName = "UICommands/Upgrgaded Building Command Data")]
  public class UpgradeBuildingUICommandData : UICommandData {
    TooltipData tooltipData;
    public override TooltipData TooltipData => tooltipData;
    [SerializeField] public GemstoneDictionary baseCost;

    void OnEnable() {
      tooltipData = new TooltipData(
        "Upgrade",
        "Upgrade this building",
        baseCost
      );
    }
  }
}
