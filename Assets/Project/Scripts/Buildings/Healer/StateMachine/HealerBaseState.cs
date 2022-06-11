namespace bts {
  public abstract class HealerBaseState : State<Healer> {
    protected HealerBaseState(StateMachine<Healer> stateMachine, StateFactory<Healer> factory)
      : base(stateMachine, factory) {
    }

    public override void EnterState() { }
    public override void ExitState() { }
    public override void UpdateState() { }

    protected bool CheckSwitchState() {
      if (Context.IsOrderedToHeal) {
        ClearOrders();
        StateMachine.SwitchState(Factory.GetState(nameof(HealerHealState)));
        return true;
      }

      if (Context.IsOrderedToStop) {
        ClearOrders();
        Context.Target = null;
        StateMachine.SwitchState(Factory.GetState(nameof(HealerIdleState)));
        return true;
      }

      return false;
    }

    void ClearOrders() {
      Context.IsOrderedToStop = false;
      Context.IsOrderedToHeal = false;
    }
  }
}
