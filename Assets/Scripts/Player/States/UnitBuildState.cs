using UnityEngine;

namespace bts {
  public class UnitBuildState : UnitBaseState {
    bool InBuildRange => Vector3.Distance(Context.CurrentPosition, Context.Destination) <= Context.BuildRange;

    public UnitBuildState(UnitStateManager context, UnitStateFactory factory)
      : base(context, factory) {
    }

    public override void EnterState() {
      Context.IsOrderedToBuild = false;
      Context.AiPath.destination = Context.Destination;
    }

    public override void UpdateState() {
      if (CheckSwitchState()) {
        return;
      }

      if (InBuildRange) {
        if (Context.GemstoneStorage.CanAfford(Context.BuildingToPlace.gemstoneCosts)) {
          Context.GemstoneStorage.Discard(Context.BuildingToPlace.gemstoneCosts);
          Context.GridBuildingSystem.Build(Context.Destination, Context.BuildingToPlace);
        }
          
        Context.BuildingToPlace = null;
        Context.AiPath.destination = Context.CurrentPosition;
        SwitchState(StateFactory.Idle);
      }
    }
  }
}