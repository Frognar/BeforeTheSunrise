namespace bts {
  public class UnitIdleState : UnitBaseState {
    public UnitIdleState(UnitStateManager context, UnitStateFactory factory)
      : base(context, factory) {
    }

    public override void EnterState() {
      Context.IsIdle = true;
    }

    public override void UpdateState() {
      if (CheckSwitchState()) {
        return;
      }
    }

    public override void ExitState() {
      Context.IsIdle = false;
    }
  }
}