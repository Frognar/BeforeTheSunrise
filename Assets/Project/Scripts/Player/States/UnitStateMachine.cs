using fro.States;

namespace bts {
  public class UnitStateMachine : StateMachine<Unit> {
    readonly UnitStateFactory factory;

    public UnitStateMachine(Unit context)
      : base(context) {
      factory = new UnitStateFactory(this);
    }

    public override void Start() {
      SwitchState(factory.GetState(nameof(UnitIdleState)));
    }
  }
}