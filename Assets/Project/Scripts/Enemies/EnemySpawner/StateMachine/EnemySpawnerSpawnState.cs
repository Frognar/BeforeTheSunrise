using fro.States;
using UnityEngine;

namespace bts {
  public class EnemySpawnerSpawnState : EnemySpawnerBaseState {
    float lastSpawnTime;
    bool IsTimeToSpawn => lastSpawnTime + Context.SpawnInterval <= Time.time;
      
    public EnemySpawnerSpawnState(StateMachine<EnemySpawner> stateMachine, StateFactory<EnemySpawner> factory)
      : base(stateMachine, factory) {
    }

    public override void UpdateState() {
      if (IsTimeToSpawn) {
        lastSpawnTime = Time.time;
        _ = Context.EnemyPool.Get();
      }
    }
  }
}
