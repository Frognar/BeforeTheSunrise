using UnityEngine;

namespace bts {
  public class UnitBuildState : UnitBaseState {
    bool InBuildRange => Vector3.Distance(Context.CurrentPosition, Context.Destination) <= Context.BuildRange;
    float prevStopDistance;

    public UnitBuildState(UnitStateManager context, UnitStateFactory factory)
      : base(context, factory) {
    }

    public override void EnterState() {
      prevStopDistance = Context.AiPath.endReachedDistance;
      Context.AiPath.endReachedDistance = Context.AttackRange - 2f;
      Context.IsOrderedToBuild = false;
      Context.AiPath.destination = Context.Destination;
    }

    public override void UpdateState() {
      if (CheckSwitchState()) {
        return;
      }

      if (InBuildRange) {
        if (Context.GemstoneStorage.CanAfford((Context.BuildingToPlace.customData as CustomBuildingData).buildingCosts)) {
          Context.GemstoneStorage.Discard((Context.BuildingToPlace.customData as CustomBuildingData).buildingCosts);
          Context.GridBuildingSystem.Build(Context.Destination, Context.BuildingToPlace);
        }
          
        Context.BuildingToPlace = null;
        SwitchState(StateFactory.Idle);
      }
    }

    public override void ExitState() {
      Context.AiPath.destination = Context.CurrentPosition;
      Context.AiPath.endReachedDistance = prevStopDistance;
    }
  }
}