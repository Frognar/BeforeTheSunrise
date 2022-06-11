using UnityEngine;

namespace bts {
  public class UnitGatherState : UnitBaseState {
    float lastGatherTime;
    bool IsTimeToGather => lastGatherTime + Context.TimeBetweenGathers <= Time.time;
    bool InGatherRange => Vector3.Distance(Context.Position, TargetGemstone) <= Context.GatherRange;
    Vector3 TargetGemstone => Context.TargerGemstone.Center.position;

    public UnitGatherState(StateMachine<Unit> stateMachine, StateFactory<Unit> factory)
      : base(stateMachine, factory) {
    }

    public override void EnterState() {
      Context.Pathfinder.SetDestination(TargetGemstone);
      Context.Pathfinder.SetStopDistance(Context.GatherRange - 2f);
    }

    public override void UpdateState() {
      if (CheckSwitchState()) {
        return;
      }

      if (InGatherRange) {
        Context.IsGathering = true;
        if (IsTimeToGather) {
          lastGatherTime = Time.time;
          GemstoneType type = Context.TargerGemstone.GemstoneType;
          int count = Context.TargerGemstone.BaseGatherAmount + Context.GatherBonuses[type];
          Context.GemstoneStorage.Store(type, count);
          PopupTextParameters popupParams = new PopupTextParameters() {
            Position = Context.TargerGemstone.Center.position,
            GemstoneType = type,
            Text = $"+{count}"
          };
          
          Context.PopupTextEventChannel.RaiseSpawnEvent(PopupTextConfiguration.Default, popupParams);
        }
      }
      else {
        Context.IsGathering = false;
      }
    }

    public override void ExitState() {
      Context.Pathfinder.Reset();
      Context.IsGathering = false;
    }
  }
}