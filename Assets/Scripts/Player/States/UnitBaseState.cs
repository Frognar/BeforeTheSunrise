namespace bts {
  public abstract class UnitBaseState : State<Unit> {
    protected UnitBaseState(StateMachine<Unit> stateMachine, StateFactory<Unit> factory)
      : base(stateMachine, factory) {
    }

    public override void ExitState() { }

    protected bool CheckSwitchState() {
      if (StateMachine.Context.IsOrderedToMove) {
        ClearOrders();
        StateMachine.Context.Target = null;
        StateMachine.Context.AIDestinationSetter.target = null;
        StateMachine.Context.BuildingToPlace = null;
        StateMachine.Context.TargerGemstone = null;
        StateMachine.SwitchState(Factory.GetState(nameof(UnitMoveState)));
        return true;
      }
      
      if (StateMachine.Context.IsOrderedToBuild) {
        ClearOrders();
        StateMachine.Context.Target = null;
        StateMachine.Context.AIDestinationSetter.target = null;
        StateMachine.Context.TargerGemstone = null;
        StateMachine.SwitchState(Factory.GetState(nameof(UnitBuildState)));
        return true;
      }

      if (StateMachine.Context.IsOrderedToAttack) {
        ClearOrders();
        StateMachine.Context.BuildingToPlace = null;
        StateMachine.Context.TargerGemstone = null;
        StateMachine.SwitchState(Factory.GetState(nameof(UnitAttackState)));
        return true;
      }

      if (StateMachine.Context.IsOrderedToGather) {
        ClearOrders();
        StateMachine.Context.Target = null;
        StateMachine.Context.AIDestinationSetter.target = null;
        StateMachine.Context.BuildingToPlace = null;
        StateMachine.SwitchState(Factory.GetState(nameof(UnitGatherState)));
        return true;
      }

      return false;
    }

    void ClearOrders() {
      StateMachine.Context.IsOrderedToMove = false;
      StateMachine.Context.IsOrderedToAttack = false;
      StateMachine.Context.IsOrderedToBuild = false;
      StateMachine.Context.IsOrderedToGather = false;
    }
  }
}