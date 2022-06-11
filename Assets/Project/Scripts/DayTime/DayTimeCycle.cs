using System;
using fro.States;
using UnityEngine;

namespace bts {
  public class DayTimeCycle : MonoBehaviour {
    [SerializeField] VoidEventChannel onStartChannel;
    [field: SerializeField] public VoidEventChannel DayStarted { get; private set; }
    [field: SerializeField] public VoidEventChannel NightStarted { get; private set; }
    [field: SerializeField] public int DayDuration { get; private set; } = 80;
    [field: SerializeField] public int NightDuration { get; private set; } = 120;
    [field: SerializeField] public IntAsset ReamaningTime { get; private set; }
    [SerializeField] VoidEventChannel onSecond;
    StateMachine<DayTimeCycle> stateMachine;

    void Awake() {
      stateMachine = new DayTimeStateMachine(this);
      ReamaningTime.value = 120;
    }

    void StartCycle(object sender, EventArgs e) {
      stateMachine.Start();
      onSecond.OnEventInvoked += UpdateCycle;
    }

    void UpdateCycle(object sender, EventArgs e) {
      stateMachine.Update();
    }

    void OnEnable() {
      onStartChannel.OnEventInvoked += StartCycle;
    }

    void OnDisable() {
      onStartChannel.OnEventInvoked -= StartCycle;
      onSecond.OnEventInvoked -= UpdateCycle;
    }
  }
}
