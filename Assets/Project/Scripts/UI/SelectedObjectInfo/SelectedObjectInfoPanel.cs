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
      if (data.ContainsKey(DataType.Name)) {
        objectName.gameObject.SetActive(true);
      }
      else {
        objectName.gameObject.SetActive(false);
      }

      if (data.ContainsKey(DataType.MaxHealth) && data.ContainsKey(DataType.CurrentHealth)) {
        health.gameObject.SetActive(true);
      }
      else {
        health.gameObject.SetActive(false);
      }

      if (data.ContainsKey(DataType.MaxEnergy) && data.ContainsKey(DataType.CurrentEnergy)) {
        energy.gameObject.SetActive(true);
      }
      else {
        energy.gameObject.SetActive(false);
      }

      if (data.ContainsKey(DataType.MovementSpeed)) {
        movementSpeed.gameObject.SetActive(true);
      }
      else {
        movementSpeed.gameObject.SetActive(false);
      }

      if (data.ContainsKey(DataType.DamagePerSecond)) {
        damagePerSecond.gameObject.SetActive(true);
      }
      else {
        damagePerSecond.gameObject.SetActive(false);
      }

      if (data.ContainsKey(DataType.HealPerSecond)) {
        healPerSecond.gameObject.SetActive(true);
      }
      else {
        healPerSecond.gameObject.SetActive(false);
      }

      if (data.ContainsKey(DataType.EnergyPerSecond)) {
        energyPerSecond.gameObject.SetActive(true);
      }
      else {
        energyPerSecond.gameObject.SetActive(false);
      }

      if (data.ContainsKey(DataType.EnergyUsagePerSecond)) {
        energyUsagePerSecond.gameObject.SetActive(true);
      }
      else {
        energyUsagePerSecond.gameObject.SetActive(false);
      }

      if (data.ContainsKey(DataType.GemstoneType)) {
        gemstoneTypePerSecond.gameObject.SetActive(true);
      }
      else {
        gemstoneTypePerSecond.gameObject.SetActive(false);
      }

      if (data.ContainsKey(DataType.GatherAmount)) {
        gatherAmountPerSecond.gameObject.SetActive(true);
      }
      else {
        gatherAmountPerSecond.gameObject.SetActive(false);
      }
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
        movementSpeed.UpdateStat("Speed: ", (float)data[DataType.MovementSpeed]);
      }

      if (data.ContainsKey(DataType.DamagePerSecond)) {
        damagePerSecond.UpdateStat("Damage/s: ", (float)data[DataType.DamagePerSecond]);
      }
      
      if (data.ContainsKey(DataType.HealPerSecond)) {
        healPerSecond.UpdateStat("Heal/s: ", (float)data[DataType.HealPerSecond]);
      }

      if (data.ContainsKey(DataType.EnergyPerSecond)) {
        energyPerSecond.UpdateStat("Energy/s: ", (float)data[DataType.EnergyPerSecond]);
      }

      if (data.ContainsKey(DataType.EnergyUsagePerSecond)) {
        energyUsagePerSecond.UpdateStat("Energy usage/s: ", (float)data[DataType.EnergyUsagePerSecond]);
      }

      if (data.ContainsKey(DataType.GemstoneType)) {
        gemstoneTypePerSecond.UpdateStat("Gemstone: ", data[DataType.GemstoneType].ToString());
      }

      if (data.ContainsKey(DataType.GatherAmount)) {
        gatherAmountPerSecond.UpdateStat("Gather amount: ", (int)data[DataType.GatherAmount]);
      }
    }

    void OnDisable() {
      currentSelected.OnDataChange -= UpdateUI;
      currentSelected = null;
    }
  }
}
