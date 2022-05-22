using UnityEngine;

namespace bts {
  public class UnitGatherState : UnitBaseState {
    float lastGatherTime;
    bool IsTimeToGather => lastGatherTime + StateMachine.Context.TimeBetweenGathers <= Time.time;
    bool InGatherRange => Vector3.Distance(StateMachine.Context.CurrentPosition, StateMachine.Context.TargerGemstone.Center.position) <= StateMachine.Context.GatherRange;
    float prevStopDistance;

    public UnitGatherState(StateMachine<Unit> stateMachine, StateFactory<Unit> factory)
      : base(stateMachine, factory) {
    }

    public override void EnterState() {
      prevStopDistance = StateMachine.Context.AiPath.endReachedDistance;
      StateMachine.Context.AiPath.endReachedDistance = StateMachine.Context.GatherRange - 2f;
      StateMachine.Context.IsOrderedToGather = false;
      StateMachine.Context.AiPath.destination = StateMachine.Context.TargerGemstone.Center.position;
    }

    public override void UpdateState() {
      if (CheckSwitchState()) {
        return;
      }

      if (InGatherRange) {
        if (IsTimeToGather) {
          lastGatherTime = Time.time;
          GemstoneType type = StateMachine.Context.TargerGemstone.GemstoneType;
          StateMachine.Context.GemstoneStorage.Store(type, StateMachine.Context.TargerGemstone.BaseGatherAmount + StateMachine.Context.GatherBonuses[type]);
        }
      }
    }

    public override void ExitState() {
      StateMachine.Context.AiPath.endReachedDistance = prevStopDistance;
    }
  }
}