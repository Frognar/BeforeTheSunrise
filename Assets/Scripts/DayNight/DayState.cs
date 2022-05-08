using System;

namespace bts {
  public class DayState : DayNightBaseState {
    public static event EventHandler OnDayStarted;

    public DayState(int duration) : base(duration) {
    }

    public override void EnterState(DayNighStateManager context) {
      OnDayStarted?.Invoke(this, EventArgs.Empty);
    }

    public override void ExitState(DayNighStateManager context) {
      context.SwitchState(context.NightState);
    }
  }
}
