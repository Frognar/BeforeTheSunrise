namespace bts {
  public class EnemySpawnerWaitState : EnemySpawnerBaseState {
    public EnemySpawnerWaitState(StateMachine<EnemySpawner> stateMachine, StateFactory<EnemySpawner> factory)
      : base(stateMachine, factory) {
    }
  }
}
