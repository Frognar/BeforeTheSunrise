using UnityEngine;

namespace bts {
  public class UnitGatherState : UnitBaseState {
    float lastGatherTime;
    bool IsTimeToGather => lastGatherTime + Context.TimeBetweenGathers <= Time.time;
    bool InGatherRange => Vector3.Distance(Context.CurrentPosition, Context.TargerGemstone.Position) <= Context.GatherRange;
    float prevStopDistance;

    public UnitGatherState(UnitStateManager context, UnitStateFactory factory)
      : base(context, factory) {
    }

    public override void EnterState() {
      prevStopDistance = Context.AiPath.endReachedDistance;
      Context.AiPath.endReachedDistance = Context.GatherRange - 2f;
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

    public override void ExitState() {
      Context.AiPath.endReachedDistance = prevStopDistance;
    }
  }
}