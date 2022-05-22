using UnityEngine;

namespace bts {
  [CreateAssetMenu(fileName = "TooltipSystem", menuName = "TooltipSystem")]
  public class TooltipSystem : ScriptableObject {
    public Tooltip Tooltip { get; set; }

    public void Show(TooltipData data) {
      if (Tooltip != null) {
        Tooltip.SetTooltip(data);
        Tooltip.gameObject.SetActive(true);
      }        
    }

    public void Hide() {
      if (Tooltip != null) {
        Tooltip.gameObject.SetActive(false);
      }
    }
  }
}
