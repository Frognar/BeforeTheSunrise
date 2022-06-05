using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace bts {
  public class SelectedObjectInfoPanel : MonoBehaviour {
    [SerializeField] TMP_Text objectName;
    [SerializeField] UIBar health;
    [SerializeField] UIBar energy;
    [SerializeField] UIStat movementSpeed;
    [SerializeField] UIStat damagePerSecond;
    [SerializeField] UIStat healPerSecond;
    [SerializeField] UIStat energyPerSecond;
    [SerializeField] UIStat energyUsagePerSecond;
    [SerializeField] UIStat gemstoneTypePerSecond;
    [SerializeField] UIStat gatherAmountPerSecond;
    [SerializeField] UIStat aura;
    Selectable currentSelected;

    public void SetUI(Selectable selected) {
      if (currentSelected != null) {
        currentSelected.OnDataChange -= UpdateUI;
      }

      currentSelected = selected;
      currentSelected.OnDataChange += UpdateUI;
      Dictionary<DataType, object> selectedData = currentSelected.GetData();
      ConfigureUIElements(selectedData);
      UpdateUI(selectedData);
    }

    void ConfigureUIElements(Dictionary<DataType, object> data) {
      objectName.gameObject.SetActive(data.ContainsKey(DataType.Name));
      health.gameObject.SetActive(data.ContainsKey(DataType.MaxHealth) && data.ContainsKey(DataType.CurrentHealth));
      energy.gameObject.SetActive(data.ContainsKey(DataType.MaxEnergy) && data.ContainsKey(DataType.CurrentEnergy));
      movementSpeed.gameObject.SetActive(data.ContainsKey(DataType.MovementSpeed));
      damagePerSecond.gameObject.SetActive(data.ContainsKey(DataType.DamagePerSecond));
      healPerSecond.gameObject.SetActive(data.ContainsKey(DataType.HealPerSecond));
      energyPerSecond.gameObject.SetActive(data.ContainsKey(DataType.EnergyPerSecond));
      energyUsagePerSecond.gameObject.SetActive(data.ContainsKey(DataType.EnergyUsagePerSecond));
      gemstoneTypePerSecond.gameObject.SetActive(data.ContainsKey(DataType.GemstoneType));
      gatherAmountPerSecond.gameObject.SetActive(data.ContainsKey(DataType.GatherAmount));
      aura.gameObject.SetActive(data.ContainsKey(DataType.Aura));
    }

    public void UpdateUI(Dictionary<DataType, object> data) {
      if (data.ContainsKey(DataType.Name)) {
        objectName.text = (string)data[DataType.Name];
      }

      if (data.ContainsKey(DataType.MaxHealth) && data.ContainsKey(DataType.CurrentHealth)) {
        health.UpdateBar((float)data[DataType.CurrentHealth], (float)data[DataType.MaxHealth]);
      }

      if (data.ContainsKey(DataType.MaxEnergy) && data.ContainsKey(DataType.CurrentEnergy)) {
        energy.UpdateBar((float)data[DataType.CurrentEnergy], (float)data[DataType.MaxEnergy]);
      }

      if (data.ContainsKey(DataType.MovementSpeed)) {
        movementSpeed.UpdateStat("Speed: ", $"{(float)data[DataType.MovementSpeed]:0.00}");
      }

      if (data.ContainsKey(DataType.DamagePerSecond)) {
        damagePerSecond.UpdateStat("Damage/s: ", $"{(float)data[DataType.DamagePerSecond]:0.00}");
      }

      if (data.ContainsKey(DataType.HealPerSecond)) {
        healPerSecond.UpdateStat("Heal/s: ", $"{(float)data[DataType.HealPerSecond]:0.00}");
      }

      if (data.ContainsKey(DataType.EnergyPerSecond)) {
        energyPerSecond.UpdateStat("Energy/s: ", $"{(float)data[DataType.EnergyPerSecond]:0.00}");
      }

      if (data.ContainsKey(DataType.EnergyUsagePerSecond)) {
        energyUsagePerSecond.UpdateStat("Energy usage/s: ", $"{(float)data[DataType.EnergyUsagePerSecond]:0.00}");
      }

      if (data.ContainsKey(DataType.GemstoneType)) {
        gemstoneTypePerSecond.UpdateStat("Gemstone: ", data[DataType.GemstoneType].ToString());
      }

      if (data.ContainsKey(DataType.GatherAmount)) {
        gatherAmountPerSecond.UpdateStat("Gather amount: ", (int)data[DataType.GatherAmount]);
      }

      if (data.ContainsKey(DataType.Aura)) {
        switch ((BoostType)data[DataType.Aura]) {
          case BoostType.Range:
            aura.UpdateStat("Aura: ", "Range");
            break;
          case BoostType.Damage:
            aura.UpdateStat("Aura: ", "Damage");
            break;
          case BoostType.AttackSpeed:
            aura.UpdateStat("Aura: ", "Attack speed");
            break;
        }
      }
    }

    void OnDisable() {
      currentSelected.OnDataChange -= UpdateUI;
      currentSelected = null;
    }
  }
}
