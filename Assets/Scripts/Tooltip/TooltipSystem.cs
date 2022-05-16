using System.Collections.Generic;
using UnityEngine;

namespace bts {
  public class TooltipSystem : MonoBehaviour {
    [SerializeField] Tooltip tooltip;

    public void Show(string header, string content, IDictionary<GemstoneType, int> gemstones) {
      tooltip.SetTooltip(header, content, gemstones);
      tooltip.gameObject.SetActive(true);
    }

    public void Hide() {
      tooltip.gameObject.SetActive(false);
    }
  }
}
