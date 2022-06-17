using UnityEngine;
using UnityEngine.UI;
using bts.Gemstones;

namespace bts {
  public class SelectedObjectButton : MonoBehaviour {
    [SerializeField] SelectedObjectInfoPanel infoPanel;
    [SerializeField] SelectedObjectCommandsPanel commandsPanel;
    [SerializeField] Button button;
    [SerializeField] Image background;
    [SerializeField] Image icon;
    [SerializeField] TooltipTrigger tooltip;

    public void SetUp(Selectable selected) {
      tooltip.SetUp(new TooltipData(string.Empty, selected.Name, new GemstoneDictionary()));
      icon.sprite = selected.Icon;
      button.onClick.AddListener(delegate { infoPanel.SetUI(selected); });
      button.onClick.AddListener(delegate { commandsPanel.SetCurrentSelected(selected); });
    }

    public void InvokeButtonClick() {
      button.onClick.Invoke();
    }

    void OnDisable() {
      button.onClick.RemoveAllListeners();
    }

    public void DiselectButton() {
      background.color = Color.white;
    }

    public void SelectButton() {
      background.color = Color.green;
    }
  }
}
