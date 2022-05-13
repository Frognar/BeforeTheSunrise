using UnityEngine;

namespace bts {
  public class UnitCommander : MonoBehaviour {
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
        if (Physics.Raycast(playerInputs.RayToWorld, out RaycastHit hitInfo)) {
          Command command;
          if (buildingToPlace != null) {
            command = new UnitBuildCommand(unit, buildingToPlace, hitInfo.point);
            if (!playerInputs.IsCommandQueuingEnabled) {
              ClearBuildingToBuild();
            }
          }
          else if (hitInfo.transform.TryGetComponent(out Damageable damageable) && damageable.ObjectAffiliation != Affiliation.Player) {
            command = new UnitAttackCommand(unit, damageable);
          }
          else if (hitInfo.transform.TryGetComponent(out Gemstone gemstone)) {
            command = new UnitGatherCommand(unit, gemstone);
          }
          else {
            command = new UnitMoveCommand(unit, hitInfo.point);
          }

          if (!playerInputs.IsCommandQueuingEnabled) {
            invoker.ForceCommandExecution(command);
          }
          else {
            invoker.AddCommand(command);
          }
        }
      }
      else if (!unit.IsSelected) {
        ClearBuildingToBuild();
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