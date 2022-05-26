using UnityEngine;

namespace bts {
  public class UnitGatherState : UnitBaseState {
    float lastGatherTime;
    bool IsTimeToGather => lastGatherTime + StateMachine.Context.TimeBetweenGathers <= Time.time;
    bool InGatherRange => Vector3.Distance(StateMachine.Context.Position, TargetGemstone) <= StateMachine.Context.GatherRange;
    Vector3 TargetGemstone => StateMachine.Context.TargerGemstone.Center.position;

    public UnitGatherState(StateMachine<Unit> stateMachine, StateFactory<Unit> factory)
      : base(stateMachine, factory) {
    }

    public override void EnterState() {
      StateMachine.Context.Pathfinder.SetDestination(TargetGemstone);
      StateMachine.Context.Pathfinder.SetStopDistance(StateMachine.Context.GatherRange - 2f);
    }

    public override void UpdateState() {
      if (CheckSwitchState()) {
        return;
      }

      if (InGatherRange) {
        StateMachine.Context.IsGathering = true;
        if (IsTimeToGather) {
          lastGatherTime = Time.time;
          GemstoneType type = StateMachine.Context.TargerGemstone.GemstoneType;
          StateMachine.Context.GemstoneStorage.Store(type, StateMachine.Context.TargerGemstone.BaseGatherAmount + StateMachine.Context.GatherBonuses[type]);
        }
      }
      else {
        StateMachine.Context.IsGathering = false;
      }
    }

    public override void ExitState() {
      StateMachine.Context.Pathfinder.Reset();
      StateMachine.Context.IsGathering = false;
    }
  }
}