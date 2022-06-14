using System;
using fro.ValueAssets;
using TMPro;
using UnityEngine;

namespace bts {
  public class TimerUI : MonoBehaviour {
    [SerializeField] VoidEventChannel onSecond;
    [SerializeField] IntAsset reamaningTime;
    [SerializeField] TextMeshProUGUI timerText;

    void Start() {
      UpdateTimer(null, EventArgs.Empty);
    }

    void OnEnable() {
      onSecond.OnEventInvoked += UpdateTimer;
    }

    void OnDisable() {
      onSecond.OnEventInvoked -= UpdateTimer;
    }

    void UpdateTimer(object sender, EventArgs e) {
      timerText.text = reamaningTime.value + "s";
    }
  }
}
