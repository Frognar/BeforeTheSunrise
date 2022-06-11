using System.Collections.Generic;
using fro.States;

namespace bts {
  public class DayTimeStateFactory : StateFactory<DayTimeCycle> {
    public DayTimeStateFactory(DayTimeStateMachine stateMachine)
      : base(stateMachine) {
    }

    protected override Dictionary<string, State<DayTimeCycle>> CreateStates() {
      return new Dictionary<string, State<DayTimeCycle>> {
        { nameof(DayState), new DayState(StateMachine, this, StateMachine.Context.DayDuration) },
        { nameof(NightState), new NightState(StateMachine, this, StateMachine.Context.NightDuration) }
      };
    }
  }
}