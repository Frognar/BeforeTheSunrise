using System.Linq;
using UnityEngine;

namespace bts {
  public class SelectedObjectUI : MonoBehaviour {
    SelectedObjectInfoPanel infoPanel;
    UICommandsPanel commandsPanel;
    Player player;

    void Awake() {
      infoPanel = GetComponentInChildren<SelectedObjectInfoPanel>();
      infoPanel.gameObject.SetActive(false);
      commandsPanel = GetComponentInChildren<UICommandsPanel>();
      commandsPanel.gameObject.SetActive(false);
      player = FindObjectOfType<Player>();
    }

    void OnEnable() {
      player.OnSelection += UpdateUI;  
    }

    void OnDisable() {
      player.OnSelection -= UpdateUI;
    }

    void UpdateUI(object sender, Player.OnSelectionEventArgs e) {
      bool isSomethingSelected = e.Selected.Count > 0;
      infoPanel.gameObject.SetActive(isSomethingSelected);
      commandsPanel.gameObject.SetActive(false);
      if (isSomethingSelected) {
        infoPanel.UpdateUI(e);
        Selectable selected = e.Selected.First();
        if (selected.UICommands.Any()) {
          commandsPanel.gameObject.SetActive(true);
          commandsPanel.UpdateUI(selected);
        }
      }
    }
  }
}
