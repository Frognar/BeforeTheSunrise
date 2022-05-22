using UnityEngine;

namespace bts {
  public class UnitCommander : MonoBehaviour {
    [SerializeField] BoolAsset canBuild;
    [SerializeField] BoolAsset inBuildMode;
    [SerializeField] GhostObject currentGhost;
    Unit unit;
    UnitCommandInvoker invoker;
    PlayerInputs playerInputs;
    PlacedObjectTypeSO buildingToPlace;

    void Awake() {
      unit = FindObjectOfType<Unit>();
      invoker = FindObjectOfType<UnitCommandInvoker>();
      playerInputs = FindObjectOfType<PlayerInputs>();
      currentGhost.gameObject.SetActive(false);
    }

    void Update() {
      if (playerInputs.Canceled) {
        ClearBuildingToBuild();
      }

      inBuildMode.value = buildingToPlace != null;
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
      if (canBuild && unit.GemstoneStorage.CanAfford((buildingToPlace.customData as CustomBuildingData).buildingCosts)) {
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
      currentGhost.gameObject.SetActive(false);
    }

    public void SetBuildingToBuild(PlacedObjectTypeSO buildingType) {
      if (unit.IsSelected) {
        buildingToPlace = buildingType;
        currentGhost.SetUp(buildingType);
        currentGhost.gameObject.SetActive(true);
      }
    }
  }
}