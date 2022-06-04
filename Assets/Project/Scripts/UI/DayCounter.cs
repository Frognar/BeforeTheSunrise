using System;
using TMPro;
using UnityEngine;

namespace bts {
  public class DayCounter : MonoBehaviour {
    [SerializeField] IntAsset currentDay;
    [SerializeField] TMP_Text counterUI;
    [SerializeField] VoidEventChannel onDayStarted;

    void OnEnable() {
      currentDay.value = 0;
      onDayStarted.OnEventInvoked += UpdateCounter;
    }

    void OnDisable() {
      onDayStarted.OnEventInvoked -= UpdateCounter;
    }

    void UpdateCounter(object sender, EventArgs e) {
      currentDay.value++;
      counterUI.text = currentDay.value.ToString();
    }
  }
}
