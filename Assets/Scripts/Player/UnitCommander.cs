using UnityEngine;

namespace bts {
  public class UnitCommander : MonoBehaviour {
    Unit unit;
    PlayerInputs playerInputs;
    PlacedObjectTypeSO buildingToPlace;
    GhostObject currentGhost;

    void Awake() {
      unit = FindObjectOfType<Unit>();
      playerInputs = FindObjectOfType<PlayerInputs>();
    }

    void Update() {
      if (unit.IsSelected && playerInputs.IsRightBtnDawn) {
        if (Physics.Raycast(playerInputs.GetRayFromMouseToWorld(), out RaycastHit hitInfo)) {
          if (buildingToPlace != null) {
            unit.Build(buildingToPlace, hitInfo.point);
            ClearBuildingToBuild();
          }
          else if (hitInfo.transform.TryGetComponent(out Damageable damageable) && damageable.ObjectAffiliation != Affiliation.Player) {
            unit.Attack(damageable);
          }
          else {
            unit.MoveTo(hitInfo.point);
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