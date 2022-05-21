using UnityEngine;

namespace bts {
  public abstract class UICommandData : ScriptableObject {
    [field: SerializeField] public Sprite ButtonIcon { get; private set; }
    public abstract TooltipData TooltipData { get; }
  }

  [CreateAssetMenu(fileName = "SimpleCommandData", menuName = "UICommands/Simple Command Data")]
  public class SimpleUICommandData : UICommandData {
    [SerializeField] TooltipData tooltipData;
    public override TooltipData TooltipData => tooltipData;
  }
}
