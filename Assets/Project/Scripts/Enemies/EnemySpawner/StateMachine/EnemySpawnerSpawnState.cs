using UnityEngine;

namespace bts {
  public class EnemySpawnerSpawnState : EnemySpawnerBaseState {
    float lastSpawnTime;
    bool IsTimeToSpawn => lastSpawnTime + Context.SpawnInterval <= Time.time;
      
    public EnemySpawnerSpawnState(StateMachine<EnemySpawner> stateMachine, StateFactory<EnemySpawner> factory)
      : base(stateMachine, factory) {
    }

    public override void EnterState() {
      Context.EnemyData.Damage *= Mathf.Pow(1.15f, Context.DayCounter - 1);
      Context.EnemyData.MaxHealth *= Mathf.Pow(1.15f, Context.DayCounter - 1);
    }

    public override void UpdateState() {
      if (IsTimeToSpawn) {
        lastSpawnTime = Time.time;
        _ = Context.EnemyPool.Get();
      }
    }
  }
}
