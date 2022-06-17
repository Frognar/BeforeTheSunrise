using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace bts {
  public class SelectedObjectUI : MonoBehaviour {
    [SerializeField] SelectablesEventChannel selectablesEventChannel;
    [SerializeField] SelectedObjectInfoPanel infoPanel;
    [SerializeField] SelectedObjectsPanel selectedObjectsPanel;
    [SerializeField] SelectedObjectCommandsPanel commandsPanel;

    void Awake() {
      infoPanel.gameObject.SetActive(false);
      selectedObjectsPanel.gameObject.SetActive(false);
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
      selectedObjectsPanel.gameObject.SetActive(isSomethingSelected);
      commandsPanel.gameObject.SetActive(false);
      
      if (isSomethingSelected) {
        infoPanel.SetUI(selected.First());
        selectedObjectsPanel.SetUI(selected);
        if (selected.Any(s => s.UICommands.Any())) {
          commandsPanel.gameObject.SetActive(true);
          commandsPanel.SetUpUI(selected);
        }
      }
    }
  }
}
