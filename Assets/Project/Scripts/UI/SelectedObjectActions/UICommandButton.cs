using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace bts {
  public class UICommandButton : MonoBehaviour {
    [SerializeField] SelectedObjectCommandsPanel uiPanel;
    [SerializeField] Button button;
    [SerializeField] Image icon;
    [SerializeField] TooltipTrigger tooltip;
    
    public void SetUp(List<UICommand> commands) {
      if (commands.Count > 0) {
        UICommand first = commands.First();
        tooltip.SetUp(first.TooltipData);
        icon.sprite = first.ButtonIcon;
        button.interactable = first.CanExecute();
        button.onClick.AddListener(delegate {
          commands.ForEach(c => c.Execute());
          tooltip.SetUp(first.TooltipData);
          uiPanel.ReloadActionUI();
        });
      }
    }
    
    public void SetUp(UICommand command) {
      tooltip.SetUp(command.TooltipData);
      icon.sprite = command.ButtonIcon;
      button.interactable = command.CanExecute();
      button.onClick.AddListener(delegate {
        command.Execute();
        tooltip.SetUp(command.TooltipData);
        uiPanel.ReloadActionUI();
      });
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
