using System;
using TMPro;
using UnityEngine;

namespace bts {
  public class TimerUI : MonoBehaviour {
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] DayNightCycle dayNightCycle;

    void Start() {
      UpdateTimer();
    }

    void OnEnable() {
      TimeTicker.OnSecond += UpdateTimer;
    }

    void OnDisable() {
      TimeTicker.OnSecond -= UpdateTimer;
    }

    void UpdateTimer(object sender, EventArgs e) {
      UpdateTimer();
    }

    void UpdateTimer() {
      timerText.text = dayNightCycle.GetReamaningTime() + "s";
    }
  }
}
