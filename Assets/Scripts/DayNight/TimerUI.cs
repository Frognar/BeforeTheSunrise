using System;
using TMPro;
using UnityEngine;

namespace bts {
  public class TimerUI : MonoBehaviour {
    [SerializeField] TextMeshProUGUI timerText;
    
    DayNighStateManager dayNightCycle;

    void Awake() {
      dayNightCycle = FindObjectOfType<DayNighStateManager>();
    }

    void Start() {
      UpdateTimer(null, EventArgs.Empty);
    }

    void OnEnable() {
      TimeTicker.OnSecond += UpdateTimer;
    }

    void OnDisable() {
      TimeTicker.OnSecond -= UpdateTimer;
    }

    void UpdateTimer(object sender, EventArgs e) {
      timerText.text = dayNightCycle.ReamaningTime + "s";
    }
  }
}
