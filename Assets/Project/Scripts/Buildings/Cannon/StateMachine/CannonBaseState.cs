namespace bts {
  public abstract class CannonBaseState : State<Cannon> {
    protected CannonBaseState(StateMachine<Cannon> stateMachine, StateFactory<Cannon> factory)
      : base(stateMachine, factory) {
    }

    public override void EnterState() { }
    public override void ExitState() { }

    protected bool CheckSwitchState() {
      if (Context.IsOrderedToAttack) {
        ClearOrders();
        StateMachine.SwitchState(Factory.GetState(nameof(CannonAttackState)));
        return true;
      }

      if (Context.IsOrderedToStop) {
        ClearOrders();
        Context.Target = null;
        StateMachine.SwitchState(Factory.GetState(nameof(CannonIdleState)));
        return true;
      }

      return false;
    }

    void ClearOrders() {
      Context.IsOrderedToStop = false;
      Context.IsOrderedToAttack = false;
    }
  }
}
