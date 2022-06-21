using UnityEngine;

namespace bts {
  public class SpecialRewardButton : RewardButton {
    [SerializeField] string customTooltipContent;
    [SerializeField] MonoBehaviour moduleToEnable;

    protected override TooltipData CreateTooltip() {
      return new TooltipData(string.Empty, BaseTooltipContent + "\n" + customTooltipContent, null);
    }

    public override void Select() {
      base.Select();
      moduleToEnable.enabled = true;
    }

    public override void Deselect() {
      base.Deselect();
      moduleToEnable.enabled = false;
    }
  }
}
