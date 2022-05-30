using UnityEngine;

namespace bts {
  [CreateAssetMenu(fileName = "SimpleCommandData", menuName = "UICommands/Simple Command Data")]
  public class SimpleUICommandData : UICommandData {
    [SerializeField] TooltipData tooltipData;
    public override TooltipData TooltipData => tooltipData;
  }
}
