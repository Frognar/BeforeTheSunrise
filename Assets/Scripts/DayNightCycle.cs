using System;
using UnityEngine;

namespace bts {
  public class DayNightCycle : MonoBehaviour {
    public static event EventHandler<DayTime> DayTimeChanged;

    [SerializeField] int dayDuration = 80;
    [SerializeField] int nightDuration = 120;

    int reaminingTime;
    DayTime dayTime;

    void Awake() {
      dayTime = DayTime.Day;
      reaminingTime = dayDuration;
    }

    void OnEnable() {
      TimeTicker.OnSecond += UpdateCycle;
    }

    void OnDisable() {
      TimeTicker.OnSecond -= UpdateCycle;
    }

    void UpdateCycle(object sender, EventArgs e) {
      if (--reaminingTime <= 0) {
        if (dayTime == DayTime.Day) {
          SetNight();
        }
        else {
          SetDay();
        }
      }
    }

    void SetDay() {
      dayTime = DayTime.Day;
      reaminingTime = dayDuration;
      DayTimeChanged?.Invoke(this, dayTime);
    }

    void SetNight() {
      dayTime = DayTime.Night;
      reaminingTime = nightDuration;
      DayTimeChanged?.Invoke(this, dayTime);
    }

    public int GetReamaningTime() {
      return reaminingTime;
    }
  }
}
