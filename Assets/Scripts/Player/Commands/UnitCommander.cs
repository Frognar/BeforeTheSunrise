using UnityEngine;

namespace bts {
  public class UnitCommander : MonoBehaviour {
    [SerializeField] InputReader inputReader;
    [SerializeField] BoolAsset canBuild;
    [SerializeField] BoolAsset inBuildMode;
    [SerializeField] GhostObject currentGhost;
    Unit unit;
    UnitCommandInvoker invoker;
    PlacedObjectType buildingToPlace;

    void Awake() {
      unit = GetComponent<Unit>();
      invoker = GetComponent<UnitCommandInvoker>();
      currentGhost.gameObject.SetActive(false);
    }

    void OnEnable() {
      inputReader.CancelEvent += ClearBuildingToBuild;
      inputReader.SendBuildCommandEvent += HandleBuildCommand;
      inputReader.SendCommandEvent += HandleSendingCommands;
    }

    void OnDisable() {
      inputReader.CancelEvent -= ClearBuildingToBuild;
      inputReader.SendBuildCommandEvent -= HandleBuildCommand;
      inputReader.SendCommandEvent -= HandleSendingCommands;
    }

    void Update() {
      inBuildMode.value = buildingToPlace != null;
      if (!unit.IsSelected) {
        ClearBuildingToBuild();
      }
    }

    void HandleSendingCommands(Ray rayToWorld) {
      if (unit.IsSelected) {
        if (Physics.Raycast(rayToWorld, out RaycastHit hitInfo)) {
          if (hitInfo.transform.TryGetComponent(out Damageable damageable) && damageable.ObjectAffiliation != Affiliation.Player) {
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
    }

    void HandleBuildCommand(Vector3 position) {
      if (unit.IsSelected) {
        if (canBuild && unit.GemstoneStorage.CanAfford((buildingToPlace.customData as CustomBuildingData).buildingCosts)) {
          SendCommand(new UnitBuildCommand(unit, buildingToPlace, position));
          if (!inputReader.IsCommandQueuingEnabled) {
            ClearBuildingToBuild();
          }
        }
      }
    }

    void SendCommand(Command command) {
      if (inputReader.IsCommandQueuingEnabled) {
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

    public void SetBuildingToBuild(PlacedObjectType buildingType) {
      if (unit.IsSelected) {
        buildingToPlace = buildingType;
        currentGhost.SetUp(buildingType);
        currentGhost.gameObject.SetActive(true);
      }
    }
  }
}