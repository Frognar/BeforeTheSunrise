using fro.States;

namespace bts {
  public class DayTimeStateMachine : StateMachine<DayTimeCycle> {
    readonly DayTimeStateFactory factory;
    
    public DayTimeStateMachine(DayTimeCycle context)
      : base(context) {
      factory = new DayTimeStateFactory(this);
    }

    public override void Start() {
      SwitchState(factory.GetState(nameof(DayState)));
    }
  }
}