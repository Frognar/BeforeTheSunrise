namespace bts {
  public abstract class CannonBaseState {
    protected Cannon Context { get; }
    protected ElectricDevice ElectricContext { get; }
    protected CannonStateFactory Factory { get; }

    protected CannonBaseState(Cannon context, CannonStateFactory factory) {
      Context = context;
      ElectricContext = context;
      Factory = factory;
    }

    public virtual void EnterState() { }

    public abstract void UpdateState();

    public virtual void ExitState() { }

    protected void SwitchState(CannonBaseState state) {
      ExitState();
      Context.SwitchState(state);
    }

    protected bool CheckSwitchState() {
      if (Context.IsOrderedToAttack) {
        ClearOrders();
        SwitchState(Factory.Attack);
        return true;
      }

      if (Context.IsOrderedToStop) {
        ClearOrders();
        Context.Target = null;
        SwitchState(Factory.Idle);
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
