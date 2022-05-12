namespace bts {
  public abstract class UnitBaseState {
    protected UnitStateManager Context { get; private set; }
    protected UnitStateFactory StateFactory { get; private set; }
    public UnitBaseState(UnitStateManager context, UnitStateFactory factory) {
      Context = context;
      StateFactory = factory;
    }

    public virtual void EnterState() { }

    public virtual void UpdateState() { }

    public virtual void ExitState() { }

    protected void SwitchState(UnitBaseState newState) {
      ExitState();
      Context.SwitchState(newState);
    }

    protected bool CheckSwitchState() {
      if (Context.IsOrderedToMove) {
        ClearOrders();
        Context.Target = null;
        Context.AIDestinationSetter.target = null;
        Context.BuildingToPlace = null;
        SwitchState(StateFactory.Move);
        return true;
      }
      
      if (Context.IsOrderedToBuild) {
        ClearOrders();
        Context.Target = null;
        Context.AIDestinationSetter.target = null;
        SwitchState(StateFactory.Build);
        return true;
      }

      if (Context.IsOrderedToAttack) {
        ClearOrders();
        Context.BuildingToPlace = null;
        SwitchState(StateFactory.Attack);
        return true;
      }

      return false;
    }

    void ClearOrders() {
      Context.IsOrderedToMove = false;
      Context.IsOrderedToAttack = false;
      Context.IsOrderedToBuild = false;
    }
  }
}