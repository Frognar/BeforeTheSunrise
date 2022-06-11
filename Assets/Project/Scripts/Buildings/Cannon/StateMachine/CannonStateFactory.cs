using System.Collections.Generic;
using fro.States;

namespace bts {
  public class CannonStateFactory : StateFactory<Cannon> {
    public CannonStateFactory(StateMachine<Cannon> stateMachine)
      : base(stateMachine) {
    }

    protected override Dictionary<string, State<Cannon>> CreateStates() {
      return new Dictionary<string, State<Cannon>> {
        { nameof(CannonIdleState), new CannonIdleState(StateMachine, this) },
        { nameof(CannonAttackState), new CannonAttackState(StateMachine, this) },
      };
    }
  }
}
