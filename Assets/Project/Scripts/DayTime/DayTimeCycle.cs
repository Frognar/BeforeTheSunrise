using System;
using UnityEngine;

namespace bts {
  public class DayTimeCycle : MonoBehaviour {
    [field: SerializeField] public VoidEventChannel DayStarted { get; private set; }
    [field: SerializeField] public VoidEventChannel NightStarted { get; private set; }
    [field: SerializeField] public int DayDuration { get; private set; } = 80;
    [field: SerializeField] public int NightDuration { get; private set; } = 120;
    [field: SerializeField] public IntAsset ReamaningTime { get; private set; }
    [SerializeField] VoidEventChannel onSecond;
    StateMachine<DayTimeCycle> stateMachine;

    void Awake() {
      stateMachine = new DayTimeStateMachine(this);
    }

    void Start() {
      stateMachine.Start();
      ReamaningTime.value = 120;
    }

    void UpdateCycle(object sender, EventArgs e) {
      stateMachine.Update();
    }

    void OnEnable() {
      onSecond.OnEventInvoked += UpdateCycle;
    }

    void OnDisable() {
      onSecond.OnEventInvoked -= UpdateCycle;
    }
  }
}
