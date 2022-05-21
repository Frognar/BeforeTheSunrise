using UnityEngine;

namespace bts {
  [CreateAssetMenu(fileName = "DemolishCommandData", menuName = "UICommands/Demolish Command Data")]
  public class DemolishUICommandData : UICommandData {
    TooltipData tooltipData;
    public override TooltipData TooltipData => tooltipData;
    [field: SerializeField][field: Range(0, 1)] public float RefundRate { get; private set; } = .75f;

    void OnEnable() {
      tooltipData = new TooltipData("Demolish", $"Refund {RefundRate * 100}% of total building cost", new GemstoneDictionary());
    }
  }
}
