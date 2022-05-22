namespace bts
{
  public class CannonStateMachine : StateMachine<Cannon> {
    readonly CannonStateFactory factory;
    
    public CannonStateMachine(Cannon context)
      : base(context) {
      factory = new CannonStateFactory(this);
    }

    public override void Start() {
      SwitchState(factory.GetState(nameof(CannonIdleState)));
    }
  }
}
