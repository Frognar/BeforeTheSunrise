namespace bts {
  public class UnitStateFactory {
    UnitStateManager Context { get; }
    public UnitBaseState Idle { get; }
    public UnitBaseState Move { get; }
    public UnitBaseState Attack { get; }
    public UnitBaseState Build { get; }
    public UnitBaseState Gather { get; }

    public UnitStateFactory(UnitStateManager context) {
      Context = context;
      Idle = new UnitIdleState(Context, factory: this);
      Move = new UnitMoveState(Context, factory: this);
      Attack = new UnitAttackState(Context, factory: this);
      Build = new UnitBuildState(Context, factory: this);
      Gather = new UnitGatherState(Context, factory: this);
    }
  }
}