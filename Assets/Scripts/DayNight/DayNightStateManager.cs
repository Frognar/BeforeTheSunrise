using System;
using UnityEngine;

namespace bts {
  public class DayNightStateManager : MonoBehaviour {
    [SerializeField] int dayDuration = 80;
    [SerializeField] int nightDuration = 120;
    public int DayDuration => dayDuration;
    public int NightDuration => nightDuration;
    public int ReamaningTime => CurrentState.ReamaningTime;

    public DayNightBaseState CurrentState { get; private set; }
    DayNightStateFactory stateFactory;

    void Awake() {
      stateFactory = new DayNightStateFactory(context: this);
    }

    void Start() {
      SwitchState(stateFactory.Day);
    }

    public void SwitchState(DayNightBaseState state) {
      CurrentState = state;
      CurrentState.EnterState();
    }

    void UpdateCycle(object sender, EventArgs e) {
      CurrentState.UpdateState();
    }

    void OnEnable() {
      TimeTicker.OnSecond += UpdateCycle;
    }

    void OnDisable() {
      TimeTicker.OnSecond -= UpdateCycle;
    }
  }
}
