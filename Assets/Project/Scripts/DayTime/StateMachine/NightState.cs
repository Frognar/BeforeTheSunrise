namespace bts {
  public class NightState : DayTimeBaseState {
    public NightState(StateMachine<DayTimeCycle> stateMachine, StateFactory<DayTimeCycle> factory, int dayTimeDuration)
      : base(stateMachine, factory, dayTimeDuration) {
    }

    public override void EnterState() {
      base.EnterState();
      Context.NightStarted.Invoke();
    }

    protected override void ChangeDayTime() {
      StateMachine.SwitchState(Factory.GetState(nameof(DayState)));
    }
  }
}