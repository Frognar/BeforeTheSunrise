using bts.Gemstones;
using UnityEngine;

namespace bts {

  public class GemstoneGatherReward : RewardButton {
    [field: SerializeField] public GemstoneDictionary GemstoneReward { get; private set; }

    protected override TooltipData CreateTooltip() {
      return new TooltipData(string.Empty, BaseTooltipContent, GemstoneReward);
    }
  }
}
