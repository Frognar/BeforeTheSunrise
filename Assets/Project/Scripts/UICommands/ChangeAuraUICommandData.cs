using bts.Gemstones;
using UnityEngine;

namespace bts {
  [CreateAssetMenu(fileName = "Change Aura Command Data", menuName = "UICommands/Change Aura Command Data")]
  public class ChangeAuraUICommandData : UICommandData {
    [field: SerializeField] public string TooltipDescription { get; private set; }
    [field: SerializeField] public GemstoneStorage Storage { get; private set; }
    [field: SerializeField] public GemstoneDictionary BaseCost { get; private set; }
    [field: SerializeField] public BoostType BoostType { get; private set; }
    TooltipData tooltipData;
    public override TooltipData TooltipData => tooltipData;

    void OnEnable() {
      tooltipData = new(string.Empty, TooltipDescription, BaseCost);
    }
  }
}
