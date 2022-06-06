using UnityEngine;

namespace bts {
  [CreateAssetMenu(fileName = "Select Gem Type Command Data", menuName = "UICommands/Select Gem Type Command Data")]
  public class SelectMineGemTypeUICommandData : UICommandData {
    [SerializeField] TooltipData tooltipData;
    public override TooltipData TooltipData => tooltipData;
    [field: SerializeField] public GemstoneType GemstoneType { get; private set; }
    [field: SerializeField] public Material Material { get; private set; }
  }
}
