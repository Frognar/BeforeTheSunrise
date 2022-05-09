using System;

namespace bts {
  public class DayState : DayNightBaseState {
    public static event EventHandler OnDayStarted;

    public DayState(DayNightStateManager context, DayNightStateFactory factory)
      : base(context, factory, context.DayDuration) {
    }

    public override void EnterState() {
      OnDayStarted?.Invoke(this, EventArgs.Empty);
    }

    public override void ExitState() {
      Context.SwitchState(StateFactory.Night);
    }
  }
}
