using UnityEngine;

namespace bts {
  public class TooltipSystem : MonoBehaviour {
    [SerializeField] Tooltip tooltip;

    public void Show(TooltipData data) {
      tooltip.SetTooltip(data);
      tooltip.gameObject.SetActive(true);
    }

    public void Hide() {
      tooltip.gameObject.SetActive(false);
    }
  }
}
