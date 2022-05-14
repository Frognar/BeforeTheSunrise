using UnityEngine;

namespace bts {
  public class UnitCommander : MonoBehaviour {
    [SerializeField] BoolVariable canBuild;
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
      if (unit.IsSelected && playerInputs.SendCommand) {
        HandleSendingCommands();
      }
      else if (!unit.IsSelected) {
        ClearBuildingToBuild();
      }
    }

    void HandleSendingCommands() {
      if (Physics.Raycast(playerInputs.RayToWorld, out RaycastHit hitInfo)) {
        if (buildingToPlace != null) {
          if (canBuild) {
            SendBuildCommand(hitInfo.point);
          }
        }
        else if (hitInfo.transform.TryGetComponent(out Damageable damageable) && damageable.ObjectAffiliation != Affiliation.Player) {
          SendAttackCommand(damageable);
        }
        else if (hitInfo.transform.TryGetComponent(out Gemstone gemstone)) {
          SendGatherCommand(gemstone);
        }
        else {
          SendMoveCommand(hitInfo.point);
        }
      }
    }

    void SendBuildCommand(Vector3 position) {
      if (!playerInputs.IsCommandQueuingEnabled) {
        invoker.ForceCommandExecution(new UnitBuildCommand(unit, buildingToPlace, position));
        ClearBuildingToBuild();
      }
      else {
        invoker.AddCommand(new UnitBuildCommand(unit, buildingToPlace, position));
      }
    }

    void SendAttackCommand(Damageable target) {
      if (!playerInputs.IsCommandQueuingEnabled) {
        invoker.ForceCommandExecution(new UnitAttackCommand(unit, target));
      }
      else {
        invoker.AddCommand(new UnitAttackCommand(unit, target));
      }
    }

    void SendGatherCommand(Gemstone gemstone) {
      if (!playerInputs.IsCommandQueuingEnabled) {
        invoker.ForceCommandExecution(new UnitGatherCommand(unit, gemstone));
      }
      else {
        invoker.AddCommand(new UnitGatherCommand(unit, gemstone));
      }
    }

    void SendMoveCommand(Vector3 position) {
      if (!playerInputs.IsCommandQueuingEnabled) {
        invoker.ForceCommandExecution(new UnitMoveCommand(unit, position));
      }
      else {
        invoker.AddCommand(new UnitMoveCommand(unit, position));
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