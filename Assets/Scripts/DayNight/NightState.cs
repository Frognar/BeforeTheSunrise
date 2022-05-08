using System;

namespace bts {
  public class NightState : DayNightBaseState {
    public static event EventHandler OnNightStarted;

    public NightState(int duration) : base(duration) {
    }

    public override void EnterState(DayNighStateManager context) {
      OnNightStarted?.Invoke(this, EventArgs.Empty);
    }

    public override void ExitState(DayNighStateManager context) {
      context.SwitchState(context.DayState);
    }
  }
}
