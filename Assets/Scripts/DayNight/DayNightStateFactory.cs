namespace bts {
  public class DayNightStateFactory {
    DayNightStateManager Context { get; }
    public DayNightBaseState Day { get; private set; }
    public DayNightBaseState Night { get; private set; }

    public DayNightStateFactory(DayNightStateManager context) {
      Context = context;
      Day = new DayState(Context, this);
      Night = new NightState(Context, this);
    }
  }
}
