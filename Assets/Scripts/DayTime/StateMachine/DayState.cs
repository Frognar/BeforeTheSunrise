﻿namespace bts {
  public class DayState : DayTimeBaseState {
    public DayState(StateMachine<DayTimeCycle> stateMachine, StateFactory<DayTimeCycle> factory, int dayTimeDuration)
      : base(stateMachine, factory, dayTimeDuration) {
    }

    public override void EnterState() {
      base.EnterState();
      StateMachine.Context.DayStarted.Invoke();
    }

    protected override void ChangeDayTime() {
      StateMachine.SwitchState(Factory.GetState(nameof(NightState)));
    }
  }
}