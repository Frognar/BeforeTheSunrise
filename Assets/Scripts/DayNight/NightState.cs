using System;

namespace bts {
  public class NightState : DayNightBaseState {
    public static event EventHandler OnNightStarted;

    public NightState(DayNightStateManager context, DayNightStateFactory factory)
      : base(context, factory, context.NightDuration) {
    }

    public override void EnterState() {
      OnNightStarted?.Invoke(this, EventArgs.Empty);
    }

    public override void ExitState() {
      Context.SwitchState(StateFactory.Day);
    }
  }
}
