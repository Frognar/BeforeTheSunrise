using UnityEngine;
using UnityEngine.UI;

namespace bts {
  public class UICommandButton : MonoBehaviour {
    [SerializeField] Button button;
    [SerializeField] Image icon;
    [SerializeField] TooltipTrigger tooltip;

    public void SetUp(UICommand command) {
      tooltip.SetUp(command.TooltipData);
      icon.sprite = command.ButtonIcon;
      button.interactable = command.CanExecute();
      button.onClick.AddListener(delegate { command.Execute(); });
      button.onClick.AddListener(delegate { tooltip.SetUp(command.TooltipData); });
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
