namespace bts {
  public abstract class EnemySpawnerBaseState : State<EnemySpawner> {
    protected EnemySpawnerBaseState(StateMachine<EnemySpawner> stateMachine, StateFactory<EnemySpawner> factory)
      : base(stateMachine, factory) {
    }

    public override void EnterState() { }
    public override void UpdateState() { }
    public override void ExitState() { }
  }
}
