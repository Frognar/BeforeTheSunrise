namespace bts {
  public abstract class DayTimeBaseState : State<DayTimeCycle> {
    int DayTimeDuration { get; }

    public DayTimeBaseState(StateMachine<DayTimeCycle> stateMachine, StateFactory<DayTimeCycle> factory, int dayTimeDuration)
      : base(stateMachine, factory) {
      DayTimeDuration = dayTimeDuration;
    }
    
    public override void EnterState() {
      StateMachine.Context.ReamaningTime.value = DayTimeDuration;
    }

    public override void UpdateState() {
      if (--StateMachine.Context.ReamaningTime.value <= 0) {
        ChangeDayTime();
      }
    }

    public override void ExitState() { }

    protected abstract void ChangeDayTime();
  }
}