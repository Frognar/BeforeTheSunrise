using UnityEngine;

namespace bts {
  public class UnitMoveState : UnitBaseState {
    bool DestinationReached => Vector3.Distance(Context.Position, Context.Destination) <= Context.StopDistance;

    public UnitMoveState(StateMachine<Unit> stateMachine, StateFactory<Unit> factory)
      : base(stateMachine, factory) {
    }

    public override void EnterState() {
      Context.Pathfinder.SetDestination(Context.Destination);
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
      Context.Pathfinder.Reset();
    }
  }
}