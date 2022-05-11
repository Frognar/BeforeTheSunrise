using UnityEngine;

namespace bts {
  public class UnitMoveState : UnitBaseState {
    bool DestinationReached => Vector3.Distance(Context.CurrentPosition, Context.Destination) <= Context.StopDistance;

    public UnitMoveState(UnitStateManager context, UnitStateFactory factory)
      : base(context, factory) {
    }

    public override void EnterState() {
      Context.IsOrderedToMove = false;
      Context.AiPath.destination = Context.Destination;
    }

    public override void UpdateState() {
      if (CheckSwitchState()) {
        return;
      }

      if (DestinationReached) {
        SwitchState(StateFactory.Idle);
      }
    }
  }
}