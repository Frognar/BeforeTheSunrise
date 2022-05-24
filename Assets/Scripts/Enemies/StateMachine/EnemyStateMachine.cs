namespace bts {
  public class EnemyStateMachine : StateMachine<Enemy> {
    readonly EnemyStateFactory factory;

    public EnemyStateMachine(Enemy context)
      : base(context) {
      factory = new EnemyStateFactory(this);
    }

    public override void Start() {
      SwitchState(factory.GetState(nameof(EnemyLookingForTargetState)));
    }
  }
}
