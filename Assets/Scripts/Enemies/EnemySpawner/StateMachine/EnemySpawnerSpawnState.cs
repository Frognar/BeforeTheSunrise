using UnityEngine;

namespace bts {
  public class EnemySpawnerSpawnState : EnemySpawnerBaseState {
    float lastSpawnTime;
    bool IsTimeToSpawn => lastSpawnTime + StateMachine.Context.SpawnInterval <= Time.time;

    public EnemySpawnerSpawnState(StateMachine<EnemySpawner> stateMachine, StateFactory<EnemySpawner> factory)
      : base(stateMachine, factory) {
    }

    public override void UpdateState() {
      if (IsTimeToSpawn) {
        lastSpawnTime = Time.time;
        _ = StateMachine.Context.EnemyPool.Get();
      }
    }
  }
}
