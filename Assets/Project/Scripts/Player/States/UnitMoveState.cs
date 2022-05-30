using UnityEngine;

namespace bts {
  public class UnitMoveState : UnitBaseState {
    bool DestinationReached => Vector3.Distance(StateMachine.Context.Position, StateMachine.Context.Destination) <= StateMachine.Context.StopDistance;

    public UnitMoveState(StateMachine<Unit> stateMachine, StateFactory<Unit> factory)
      : base(stateMachine, factory) {
    }

    public override void EnterState() {
      StateMachine.Context.Pathfinder.SetDestination(StateMachine.Context.Destination);
    }

    public override void UpdateState() {
      if (CheckSwitchState()) {
        return;
      }

      if (DestinationReached) {
        StateMachine.SwitchState(Factory.GetState(nameof(UnitIdleState)));
      }
    }

    public override void ExitState() {
      StateMachine.Context.Pathfinder.Reset();
    }
  }
}