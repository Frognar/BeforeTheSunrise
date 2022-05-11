using UnityEngine;

namespace bts {
  public class UnitIdleState : UnitBaseState {
    public UnitIdleState(UnitStateManager context, UnitStateFactory factory)
      : base(context, factory) {
    }

    public override void UpdateState() {
      if (CheckSwitchState()) {
        return;
      }
    }
  }
}