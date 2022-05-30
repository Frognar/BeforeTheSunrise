namespace bts {
  public class HealerStateMachine : StateMachine<Healer> {
    readonly HealerStateFactory factory;

    public HealerStateMachine(Healer context)
      : base(context) {
      factory = new HealerStateFactory(this);
    }

    public override void Start() {
      SwitchState(factory.GetState(nameof(HealerIdleState)));
    }
  }
}
