namespace bts {
  public class NightState : DayNightBaseState {
    public NightState(DayNightStateManager context, DayNightStateFactory factory)
      : base(context, factory, context.NightDuration) {
    }

    public override void EnterState() {
      Context.NightStarted.Invoke();
    }

    public override void ExitState() {
      Context.SwitchState(StateFactory.Day);
    }
  }
}
