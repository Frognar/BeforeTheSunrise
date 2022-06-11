using fro.States;

namespace bts {
  public abstract class UnitBaseState : State<Unit> {
    protected UnitBaseState(StateMachine<Unit> stateMachine, StateFactory<Unit> factory)
      : base(stateMachine, factory) {
    }

    public override void ExitState() { }

    protected virtual bool CheckSwitchState() {
      if (Context.IsOrderedToMove) {
        ClearOrders();
        Context.Target = null;
        Context.BuildingToPlace = null;
        Context.TargerGemstone = null;
        StateMachine.SwitchState(Factory.GetState(nameof(UnitMoveState)));
        return true;
      }
      
      if (Context.IsOrderedToBuild) {
        ClearOrders();
        Context.Target = null;
        Context.TargerGemstone = null;
        StateMachine.SwitchState(Factory.GetState(nameof(UnitBuildState)));
        return true;
      }

      if (Context.IsOrderedToAttack) {
        ClearOrders();
        Context.BuildingToPlace = null;
        Context.TargerGemstone = null;
        StateMachine.SwitchState(Factory.GetState(nameof(UnitAttackState)));
        return true;
      }

      if (Context.IsOrderedToGather) {
        ClearOrders();
        Context.Target = null;
        Context.BuildingToPlace = null;
        StateMachine.SwitchState(Factory.GetState(nameof(UnitGatherState)));
        return true;
      }

      return false;
    }

    void ClearOrders() {
      Context.IsOrderedToMove = false;
      Context.IsOrderedToAttack = false;
      Context.IsOrderedToBuild = false;
      Context.IsOrderedToGather = false;
    }
  }
}