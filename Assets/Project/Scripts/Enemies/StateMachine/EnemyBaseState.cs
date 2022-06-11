using fro.States;

namespace bts {
  public abstract class EnemyBaseState : State<Enemy> {
    protected EnemyBaseState(StateMachine<Enemy> stateMachine, StateFactory<Enemy> factory)
      : base(stateMachine, factory) {
    }

    public override void EnterState() { }
    public override void UpdateState() { }
    public override void ExitState() { }
  }
}
