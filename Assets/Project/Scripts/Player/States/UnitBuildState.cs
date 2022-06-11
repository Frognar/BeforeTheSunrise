using UnityEngine;

namespace bts {
  public class UnitBuildState : UnitBaseState {
    bool InBuildRange => Vector3.Distance(Context.Position, Context.Destination) <= Context.BuildRange;

    public UnitBuildState(StateMachine<Unit> stateMachine, StateFactory<Unit> factory)
      : base(stateMachine, factory) {
    }

    public override void EnterState() {
      Context.Pathfinder.SetDestination(Context.Destination);
      Context.Pathfinder.SetStopDistance(Context.BuildRange - 2f);
    }

    public override void UpdateState() {
      if (CheckSwitchState()) {
        return;
      }

      if (InBuildRange) {
        if (Context.GemstoneStorage.CanAfford((Context.BuildingToPlace.customData as CustomBuildingData).buildingCosts)) {
          if (Context.GridBuildingSystem.CanBuild(Context.Destination, Context.BuildingToPlace)) {
            Context.GemstoneStorage.Discard((Context.BuildingToPlace.customData as CustomBuildingData).buildingCosts);
            Context.GridBuildingSystem.Build(Context.Destination, Context.BuildingToPlace);
          }
        }

        Context.BuildingToPlace = null;
        StateMachine.SwitchState(Factory.GetState(nameof(UnitIdleState)));
      }
    }

    public override void ExitState() {
      Context.Pathfinder.Reset();
    }
  }
}