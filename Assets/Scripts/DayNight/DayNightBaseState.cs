namespace bts {
  public abstract class DayNightBaseState {
    readonly int dayTimeDuration;
    public int ReamaningTime { get; private set; }
    public DayNightBaseState(int duration) {
      dayTimeDuration = duration;
      ReamaningTime = duration;
    }

    public abstract void EnterState(DayNighStateManager context);

    public void UpdateState(DayNighStateManager context) {
      if (--ReamaningTime <= 0) {
        ReamaningTime = dayTimeDuration;
        ExitState(context);
      }
    }
    
    public abstract void ExitState(DayNighStateManager context);
  }
}
