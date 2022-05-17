using UnityEngine;
using UnityEngine.UI;

namespace bts {
  public class ObjectActionButton : MonoBehaviour {
    [SerializeField] Button button;
    [SerializeField] Image icon;
    [SerializeField] TooltipTrigger tooltip;

    public void SetUp(ObjectAction action) {
      tooltip.SetUp(action.TootlipHeader, action.TootlipContent, action.TootlipGemstones);
      icon.sprite = action.ButtonIcon;
      button.onClick.AddListener(delegate { action.Action(); });
    }

    void OnDisable() {
      button.onClick.RemoveAllListeners();
    }

    public void DisableButton() {
      button.interactable = false;
    }

    public void EnableButton() {
      button.interactable = true;
    }
  }
}
