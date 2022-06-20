using bts.Gemstones;
using fro.States;
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
        Context.Pathfinder.Reset();
        if (IsTimeToGather) {
          Gather();
        }
      }
    }
    
    void Gather() {
      Context.IsGathering = true;
      lastGatherTime = Time.time;
      GemstoneType type = Context.TargerGemstone.GemstoneType;
      int count = Context.TargerGemstone.BaseGatherAmount + Context.GatherBonuses[type];
      Context.GemstoneStorage.Store(type, count);
      ShowPopup(type, count);
    }

    void ShowPopup(GemstoneType gemType, int gemCount) {
      PopupTextParameters popupParams = new PopupTextParameters() {
        Position = Context.TargerGemstone.Center.position,
        GemstoneType = gemType,
        Text = $"+{gemCount}"
      };

      Context.PopupTextEventChannel.RaiseSpawnEvent(PopupTextConfiguration.Default, popupParams);
    }

    public override void ExitState() {
      Context.Pathfinder.Reset();
      Context.IsGathering = false;
    }
  }
}