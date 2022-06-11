using System.Collections.Generic;
using fro.States;

namespace bts {
  public class HealerStateFactory : StateFactory<Healer> {
    public HealerStateFactory(StateMachine<Healer> stateMachine)
      : base(stateMachine) {
    }

    protected override Dictionary<string, State<Healer>> CreateStates() {
      return new Dictionary<string, State<Healer>> {
        { nameof(HealerIdleState), new HealerIdleState(StateMachine, factory: this) },
        { nameof(HealerHealState), new HealerHealState(StateMachine, factory: this) }
      };
    }
  }
}
