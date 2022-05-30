using System;

namespace bts {
  public class EnemySpawnerStateMachine : StateMachine<EnemySpawner> {
    readonly EnemySpawnerStateFactory factory;
    
    public EnemySpawnerStateMachine(EnemySpawner context)
      : base(context) {
      factory = new EnemySpawnerStateFactory(this);
      Context.DayStarted.OnEventInvoked += SwitchToWait;
      Context.NightStarted.OnEventInvoked += SwitchToSpawn;
    }

    ~EnemySpawnerStateMachine() {
      Context.DayStarted.OnEventInvoked -= SwitchToWait;
      Context.NightStarted.OnEventInvoked -= SwitchToSpawn;
    }

    void SwitchToWait(object s, EventArgs e) {
      SwitchState(factory.GetState(nameof(EnemySpawnerWaitState)));
    }

    void SwitchToSpawn(object s, EventArgs e) {
      SwitchState(factory.GetState(nameof(EnemySpawnerSpawnState)));
    }

    public override void Start() { }
  }
}
