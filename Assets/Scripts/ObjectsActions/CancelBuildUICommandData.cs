using UnityEngine;

namespace bts {
  [CreateAssetMenu(fileName = "CancelBuildCommandData", menuName = "UICommands/Cancel Command Data")]
  public class CancelBuildUICommandData : UICommandData {
    [SerializeField] TooltipData tooltipData;
    public override TooltipData TooltipData => tooltipData;
  }
}
