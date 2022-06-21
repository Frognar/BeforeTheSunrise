using UnityEngine;
using UnityEngine.UI;
using bts.Gemstones;
using fro.ValueAssets;
using System.Collections.Generic;

namespace bts {
  public class SelectedObjectButton : MonoBehaviour {
    [SerializeField] SelectedObjectInfoPanel infoPanel;
    [SerializeField] SelectedObjectCommandsPanel commandsPanel;
    [SerializeField] Button button;
    [SerializeField] Image background;
    [SerializeField] Image icon;
    [SerializeField] TooltipTrigger tooltip;
    [SerializeField] ColorAsset normalColor;
    [SerializeField] ColorAsset selectedColor;

    public void SetUp(Selectable selected) {
      Dictionary<DataType, object> data = selected.GetData();
      if (data.ContainsKey(DataType.Name)) {
        tooltip.SetUp(new TooltipData(string.Empty, data[DataType.Name].ToString(), new GemstoneDictionary()));
      }

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
      background.color = normalColor;
    }

    public void SelectButton() {
      background.color = selectedColor;
    }
  }
}
