using System.Collections.Generic;

namespace bts {
  public class UnitStateFactory : StateFactory<Unit> {
    public UnitStateFactory(StateMachine<Unit> stateMachine)
      : base(stateMachine) {
    }

    protected override Dictionary<string, State<Unit>> CreateStates() {
      return new Dictionary<string, State<Unit>> {
        { nameof(UnitIdleState), new UnitIdleState(StateMachine, factory: this) },
        { nameof(UnitMoveState), new UnitMoveState(StateMachine, factory: this) },
        { nameof(UnitAttackState), new UnitAttackState(StateMachine, factory: this) },
        { nameof(UnitBuildState), new UnitBuildState(StateMachine, factory: this) },
        { nameof(UnitGatherState), new UnitGatherState(StateMachine, factory: this) },
      };
    }
  }
}