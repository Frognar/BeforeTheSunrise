using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace bts {
  public class SelectedObjectInfoPanel : MonoBehaviour {
    [SerializeField] TMP_Text objectNameText;
    [SerializeField] Transform healthInfo;
    [SerializeField] Slider healthSlider;
    [SerializeField] TMP_Text healthText;
    Health selectedObjectHealth;

    public void UpdateUI(List<Selectable> selected) {
      if (selected.Count == 1) {
        objectNameText.text = selected.First().Name;
        Dictionary<string, object> data = selected.First().GetData();
        if (data.ContainsKey("Health")) {
          selectedObjectHealth = (Health)data["Health"];
          selectedObjectHealth.OnValueChange += UpdateHealthUI;
          healthInfo.gameObject.SetActive(true);
          UpdateHealthUI();
        }
        else {
          healthInfo.gameObject.SetActive(false);
        }
      }
      else {
        objectNameText.text = $"{selected.First().Name}: {selected.Count}";
      }
    }

    void UpdateHealthUI() {
      healthSlider.maxValue = selectedObjectHealth.MaxHealth;
      healthSlider.value = selectedObjectHealth.CurrentHealth;
      healthText.text = selectedObjectHealth.CurrentHealth + "/" + selectedObjectHealth.MaxHealth;
    }

    void OnDisable() {
      if (selectedObjectHealth != null) {
        selectedObjectHealth.OnValueChange -= UpdateHealthUI;
        selectedObjectHealth = null;
      }
    }
  }
}
