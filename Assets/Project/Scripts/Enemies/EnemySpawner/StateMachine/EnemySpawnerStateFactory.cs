using System.Collections.Generic;
using fro.States;

namespace bts {
  public class EnemySpawnerStateFactory : StateFactory<EnemySpawner> {
    public EnemySpawnerStateFactory(StateMachine<EnemySpawner> stateMachine)
      : base(stateMachine) {
    }

    protected override Dictionary<string, State<EnemySpawner>> CreateStates() {
      return new Dictionary<string, State<EnemySpawner>> {
        { nameof(EnemySpawnerWaitState), new EnemySpawnerWaitState(StateMachine, this) },
        { nameof(EnemySpawnerSpawnState), new EnemySpawnerSpawnState(StateMachine, this) },
      };
    }
  }
}
