using System;
using UnityEngine;

namespace bts {
  public class DayNighStateManager : MonoBehaviour {
    [SerializeField] int dayDuration = 80;
    [SerializeField] int nightDuration = 120;
    
    public DayState DayState { get; private set; }
    public NightState NightState { get; private set; }
    public int ReamaningTime => currentState.ReamaningTime;

    DayNightBaseState currentState;

    void Start() {
      DayState = new DayState(dayDuration);
      NightState = new NightState(nightDuration);
      SwitchState(DayState);
    }

    public void SwitchState(DayNightBaseState state) {
      currentState = state;
      currentState.EnterState(this);
    }

    void OnEnable() {
      TimeTicker.OnSecond += UpdateCycle;
    }

    void OnDisable() {
      TimeTicker.OnSecond -= UpdateCycle;
    }

    void UpdateCycle(object sender, EventArgs e) {
      currentState.UpdateState(this);
    }
  }
}
