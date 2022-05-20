using System.Linq;
using UnityEngine;

namespace bts {
  public class SelectedObjectUI : MonoBehaviour {
    SelectedObjectInfoPanel infoPanel;
    SelectedObjectActionsPanel actionsPanel;
    Player player;

    void Awake() {
      infoPanel = GetComponentInChildren<SelectedObjectInfoPanel>();
      infoPanel.gameObject.SetActive(false);
      actionsPanel = GetComponentInChildren<SelectedObjectActionsPanel>();
      actionsPanel.gameObject.SetActive(false);
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
      actionsPanel.gameObject.SetActive(false);
      if (isSomethingSelected) {
        infoPanel.UpdateUI(e);
        Selectable selected = e.Selected.First();
        if (selected.Actions.Any()) {
          actionsPanel.gameObject.SetActive(true);
          actionsPanel.UpdateUI(selected);
        }
      }
    }
  }
}
