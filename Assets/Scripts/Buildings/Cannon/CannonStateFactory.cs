namespace bts {
  public class CannonStateFactory {
    public CannonBaseState Idle { get; }
    public CannonBaseState Attack { get; }

    public CannonStateFactory(Cannon context) {
      Idle = new CannonIdleState(context, this);
      Attack = new CannonAttackState(context, this);
    }
  }
}
