using System;
using UnityEngine;

namespace bts {
  public class DayNightStateManager : MonoBehaviour {
    [field: SerializeField] public VoidEventChannel DayStarted { get; private set; }
    [field: SerializeField] public VoidEventChannel NightStarted { get; private set; }
    [field: SerializeField] public int DayDuration { get; private set; } = 80;
    [field: SerializeField] public int NightDuration { get; private set; } = 120;
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
