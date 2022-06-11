using System.Collections.Generic;
using fro.States;

namespace bts {
  public class EnemyStateFactory : StateFactory<Enemy> {
    public EnemyStateFactory(StateMachine<Enemy> stateMachine)
      : base(stateMachine) {
    }

    protected override Dictionary<string, State<Enemy>> CreateStates() {
      return new Dictionary<string, State<Enemy>>() {
        { nameof(EnemyLookingForTargetState), new EnemyLookingForTargetState(StateMachine, this) },
        { nameof(EnemyAttackState), new EnemyAttackState(StateMachine, this) },
      };
    }
  }
}
