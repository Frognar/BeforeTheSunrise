﻿using fro.ValueAssets;
using fro.BuildingSystem;
using UnityEngine;

namespace bts {
  public class UnitCommander : Commander<Unit> {
    [SerializeField] BoolAsset canBuild;
    [SerializeField] BoolAsset inBuildMode;
    [SerializeField] GhostObject currentGhost;
    PlacedObjectData buildingToPlace;
    CustomBuildingData customBuildingData;

    protected override void Awake() {
      base.Awake();
      currentGhost.gameObject.SetActive(false);
    }

    protected override void OnEnable() {
      base.OnEnable();
      inputReader.CancelEvent += ClearBuildingToBuild;
      inputReader.SendBuildCommandEvent += HandleBuildCommand;
    }

    protected override void OnDisable() {
      base.OnDisable();
      inputReader.CancelEvent -= ClearBuildingToBuild;
      inputReader.SendBuildCommandEvent -= HandleBuildCommand;
    }

    void Update() {
      inBuildMode.value = buildingToPlace != null;
      if (!receiver.IsSelected) {
        ClearBuildingToBuild();
      }
    }

    protected override void HandleSendingCommands(Ray rayToWorld) {
      if (receiver.IsSelected) {
        if (Physics.Raycast(rayToWorld, out RaycastHit hitInfo)) {
          if (hitInfo.transform.TryGetComponent(out Damageable damageable) && damageable.ObjectAffiliation != Affiliation.Player) {
            SendCommand(new UnitAttackCommand(receiver, damageable));
          }
          else if (hitInfo.transform.TryGetComponent(out Gemstone gemstone)) {
            SendCommand(new UnitGatherCommand(receiver, gemstone));
          }
          else {
            SendCommand(new UnitMoveCommand(receiver, hitInfo.point));
          }
        }
      }
    }

    void HandleBuildCommand(Vector3 position) {
      if (receiver.IsSelected) {
        if (canBuild && receiver.GemstoneStorage.CanAfford(customBuildingData.buildingCosts)) {
          SendCommand(new UnitBuildCommand(receiver, buildingToPlace, customBuildingData, position));
          if (!inputReader.IsCommandQueuingEnabled) {
            ClearBuildingToBuild();
          }
        }
      }
    }

    public void ClearBuildingToBuild() {
      buildingToPlace = null;
      ClearGhost();
    }

    void ClearGhost() {
      currentGhost.gameObject.SetActive(false);
    }

    public void SetBuildingToBuild(PlacedObjectData buildingType, CustomBuildingData buildingData) {
      if (receiver.IsSelected) {
        buildingToPlace = buildingType;
        customBuildingData = buildingData;
        currentGhost.SetUp(buildingType, buildingData);
        currentGhost.gameObject.SetActive(true);
      }
    }
  }
}