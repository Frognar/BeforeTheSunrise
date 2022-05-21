namespace bts {
  public abstract class DayNightBaseState {
    protected DayNightStateManager Context { get; private set; }
    protected DayNightStateFactory StateFactory { get; private set; }
    readonly int dayTimeDuration;

    public DayNightBaseState(DayNightStateManager context, DayNightStateFactory factory, int duration) {
      Context = context;
      StateFactory = factory;
      dayTimeDuration = duration;
      Context.ReamaningTime.value = duration;
    }

    public abstract void EnterState();

    public void UpdateState() {
      if (--Context.ReamaningTime.value <= 0) {
        Context.ReamaningTime.value = dayTimeDuration;
        ExitState();
      }
    }

    public abstract void ExitState();
  }
}
