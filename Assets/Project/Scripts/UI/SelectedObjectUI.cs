using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace bts {
  public class SelectedObjectUI : MonoBehaviour {
    [SerializeField] SelectablesEventChannel selectablesEventChannel;
    SelectedObjectInfoPanel infoPanel;
    UICommandsPanel commandsPanel;

    void Awake() {
      infoPanel = GetComponentInChildren<SelectedObjectInfoPanel>();
      infoPanel.gameObject.SetActive(false);
      commandsPanel = GetComponentInChildren<UICommandsPanel>();
      commandsPanel.gameObject.SetActive(false);
    }

    void OnEnable() {
      selectablesEventChannel.OnSelectionInvoked += UpdateUI;
    }

    void OnDisable() {
      selectablesEventChannel.OnSelectionInvoked -= UpdateUI;
    }

    void UpdateUI(object sender, List<Selectable> selected) {
      bool isSomethingSelected = selected.Count > 0;
      infoPanel.gameObject.SetActive(isSomethingSelected);
      commandsPanel.gameObject.SetActive(false);
      if (isSomethingSelected) {
        infoPanel.SetUI(selected.First());
        if (selected.Any(s => s.UICommands.Any())) {
          commandsPanel.gameObject.SetActive(true);
          commandsPanel.UpdateUI(selected);
        }
      }
    }
  }
}
