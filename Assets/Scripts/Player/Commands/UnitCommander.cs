using UnityEngine;

namespace bts {
  public class UnitCommander : MonoBehaviour {
    [SerializeField] BoolVariable canBuild;
    [SerializeField] BoolVariable inBuildMode;
    UnitStateManager unit;
    UnitCommandInvoker invoker;
    PlayerInputs playerInputs;
    PlacedObjectTypeSO buildingToPlace;
    GhostObject currentGhost;

    void Awake() {
      unit = FindObjectOfType<UnitStateManager>();
      invoker = FindObjectOfType<UnitCommandInvoker>();
      playerInputs = FindObjectOfType<PlayerInputs>();
    }

    void Update() {
      inBuildMode.Value = buildingToPlace != null;

      if (unit.IsSelected) {
        if (playerInputs.SendCommand) {
          HandleSendingCommands();
        }
        else if (playerInputs.SendBuildCommand) {
          if (Physics.Raycast(playerInputs.RayToWorld, out RaycastHit hitInfo)) {
            HandleBuildCommand(hitInfo.point);
          }
        }
      }
      else if (!unit.IsSelected) {
        ClearBuildingToBuild();
      }
    }

    void HandleSendingCommands() {
      if (Physics.Raycast(playerInputs.RayToWorld, out RaycastHit hitInfo)) {
        if (inBuildMode) {
          HandleBuildCommand(hitInfo.point);
        }
        else if (hitInfo.transform.TryGetComponent(out Damageable damageable) && damageable.ObjectAffiliation != Affiliation.Player) {
          SendCommand(new UnitAttackCommand(unit, damageable));
        }
        else if (hitInfo.transform.TryGetComponent(out Gemstone gemstone)) {
          SendCommand(new UnitGatherCommand(unit, gemstone));
        }
        else {
          SendCommand(new UnitMoveCommand(unit, hitInfo.point));
        }
      }
    }

    void HandleBuildCommand(Vector3 position) {
      if (canBuild && unit.GemstoneStorage.CanAfford(buildingToPlace.gemstoneCosts)) {
        SendCommand(new UnitBuildCommand(unit, buildingToPlace, position));
        if (!playerInputs.IsCommandQueuingEnabled) {
          ClearBuildingToBuild();
        }
      }
    }

    void SendCommand(Command command) {
      if (playerInputs.IsCommandQueuingEnabled) {
        invoker.AddCommand(command);
      }
      else {
        invoker.ForceCommandExecution(command);
      }
    }

    public void ClearBuildingToBuild() {
      buildingToPlace = null;
      ClearGhost();
    }

    void ClearGhost() {
      if (currentGhost != null) {
        Destroy(currentGhost.gameObject);
        currentGhost = null;
      }
    }

    public void SetBuildingToBuild(PlacedObjectTypeSO buildingType) {
      if (unit.IsSelected) {
        buildingToPlace = buildingType;
        ClearGhost();
        currentGhost = Instantiate(buildingType.ghost).GetComponent<GhostObject>();
      }
    }
  }
}