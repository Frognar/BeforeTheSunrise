using UnityEngine;

namespace bts {
  public class UnitGatherState : UnitBaseState {
    float lastGatherTime;
    bool IsTimeToGather => lastGatherTime + Context.TimeBetweenAttacks <= Time.time;
    bool InGatherRange => Vector3.Distance(Context.CurrentPosition, Context.TargerGemstone.Position) <= Context.GatherRange;

    public UnitGatherState(UnitStateManager context, UnitStateFactory factory)
      : base(context, factory) {
    }

    public override void EnterState() {
      Context.IsOrderedToGather = false;
      Context.AiPath.destination = Context.TargerGemstone.Position;
    }

    public override void UpdateState() {
      if (CheckSwitchState()) {
        return;
      }

      if (InGatherRange) {
        if (IsTimeToGather) {
          lastGatherTime = Time.time;
          GemstoneType type = Context.TargerGemstone.GemstoneType;
          Context.GemstoneStorage.Store(type, Context.TargerGemstone.BaseGatherAmount + Context.GatherBonuses[type]);
        }
      }
    }
  }
}