namespace bts {
  public class UnitIdleState : UnitBaseState {
    public UnitIdleState(StateMachine<Unit> stateMachine, StateFactory<Unit> factory)
      : base(stateMachine, factory) {
    }

    public override void EnterState() {
      StateMachine.Context.IsIdle = true;
    }

    public override void UpdateState() {
      if (CheckSwitchState()) {
        return;
      }
    }

    public override void ExitState() {
      StateMachine.Context.IsIdle = false;
    }
  }
}