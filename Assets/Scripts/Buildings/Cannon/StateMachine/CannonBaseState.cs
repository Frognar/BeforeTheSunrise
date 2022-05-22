namespace bts {
  public abstract class CannonBaseState : State<Cannon> {
    protected CannonBaseState(StateMachine<Cannon> stateMachine, StateFactory<Cannon> factory)
      : base(stateMachine, factory) {
    }

    public override void EnterState() { }
    public override void ExitState() { }

    protected bool CheckSwitchState() {
      if (StateMachine.Context.IsOrderedToAttack) {
        ClearOrders();
        StateMachine.SwitchState(Factory.GetState(nameof(CannonAttackState)));
        return true;
      }

      if (StateMachine.Context.IsOrderedToStop) {
        ClearOrders();
        StateMachine.Context.Target = null;
        StateMachine.SwitchState(Factory.GetState(nameof(CannonIdleState)));
        return true;
      }

      return false;
    }

    void ClearOrders() {
      StateMachine.Context.IsOrderedToStop = false;
      StateMachine.Context.IsOrderedToAttack = false;
    }
  }
}
