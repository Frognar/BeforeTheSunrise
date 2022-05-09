namespace bts {
  public abstract class DayNightBaseState {
    public int ReamaningTime { get; private set; }
    protected DayNightStateManager Context { get; private set; }
    protected DayNightStateFactory StateFactory { get; private set; }
    readonly int dayTimeDuration;

    public DayNightBaseState(DayNightStateManager context, DayNightStateFactory factory, int duration) {
      Context = context;
      StateFactory = factory;
      dayTimeDuration = duration;
      ReamaningTime = dayTimeDuration;
    }

    public abstract void EnterState();

    public void UpdateState() {
      if (--ReamaningTime <= 0) {
        ReamaningTime = dayTimeDuration;
        ExitState();
      }
    }

    public abstract void ExitState();
  }
}
