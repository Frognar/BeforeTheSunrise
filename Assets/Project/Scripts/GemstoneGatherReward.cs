using UnityEngine;

namespace bts {
  public class GemstoneGatherReward : MonoBehaviour {
    [field: SerializeField] public GemstoneDictionary GemstoneReward { get; private set; }
    [SerializeField] TooltipTrigger trigger;

    void Start() {
      trigger.UpdateGemstones(GemstoneReward);
    }
  }
}
