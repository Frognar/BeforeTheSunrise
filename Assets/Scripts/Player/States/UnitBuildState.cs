using UnityEngine;

namespace bts {
  public class UnitBuildState : UnitBaseState {
    bool InBuildRange => Vector3.Distance(StateMachine.Context.CurrentPosition, StateMachine.Context.Destination) <= StateMachine.Context.BuildRange;
    float prevStopDistance;

    public UnitBuildState(StateMachine<Unit> stateMachine, StateFactory<Unit> factory)
      : base(stateMachine, factory) {
    }

    public override void EnterState() {
      prevStopDistance = StateMachine.Context.AiPath.endReachedDistance;
      StateMachine.Context.AiPath.endReachedDistance = StateMachine.Context.AttackRange - 2f;
      StateMachine.Context.IsOrderedToBuild = false;
      StateMachine.Context.AiPath.destination = StateMachine.Context.Destination;
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
      StateMachine.Context.AiPath.destination = StateMachine.Context.CurrentPosition;
      StateMachine.Context.AiPath.endReachedDistance = prevStopDistance;
    }
  }
}