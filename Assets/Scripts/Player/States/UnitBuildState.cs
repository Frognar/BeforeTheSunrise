using UnityEngine;

namespace bts {
  public class UnitBuildState : UnitBaseState {
    bool InBuildRange => Vector3.Distance(StateMachine.Context.Position, StateMachine.Context.Destination) <= StateMachine.Context.BuildRange;

    public UnitBuildState(StateMachine<Unit> stateMachine, StateFactory<Unit> factory)
      : base(stateMachine, factory) {
    }

    public override void EnterState() {
      StateMachine.Context.Pathfinder.SetDestination(StateMachine.Context.Destination);
      StateMachine.Context.Pathfinder.SetStopDistance(StateMachine.Context.BuildRange - 2f);
    }

    public override void UpdateState() {
      if (CheckSwitchState()) {
        return;
      }

      if (InBuildRange) {
        if (StateMachine.Context.GemstoneStorage.CanAfford((StateMachine.Context.BuildingToPlace.customData as CustomBuildingData).buildingCosts)) {
          StateMachine.Context.GemstoneStorage.Discard((StateMachine.Context.BuildingToPlace.customData as CustomBuildingData).buildingCosts);
          StateMachine.Context.GridBuildingSystem.Build(StateMachine.Context.Destination, StateMachine.Context.BuildingToPlace);
        }

        StateMachine.Context.BuildingToPlace = null;
        StateMachine.SwitchState(Factory.GetState(nameof(UnitIdleState)));
      }
    }

    public override void ExitState() {
      StateMachine.Context.Pathfinder.Reset();
    }
  }
}