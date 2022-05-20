namespace bts {
  public class DayState : DayNightBaseState {
    public DayState(DayNightStateManager context, DayNightStateFactory factory)
      : base(context, factory, context.DayDuration) {
    }

    public override void EnterState() {
      Context.DayStarted.Invoke();
    }

    public override void ExitState() {
      Context.SwitchState(StateFactory.Night);
    }
  }
}
