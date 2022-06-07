using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace bts {
  public class Aura : Building, ElectricDevice {
    [SerializeField] IntAsset ticksPerSecond;
    [SerializeField] List<ChangeAuraUICommandData> changeAuraUICommandData;
    [SerializeField] VoidEventChannel onTick;
    [SerializeField] GameObject rangeVisuals;
    public float Range => auraData.range;
    public float MaxEnergy => auraData.maxEnergy;
    public float EnergyPerDevice => auraData.energyPerDevicePerSecond / ticksPerSecond;
    public float CurrentEnergy { get; private set; }
    BoostType BoostType { get; set; } = BoostType.Damage;

    float BoostMultiplier => 1.1f * Mathf.Pow(1.15f, BuildingLevel);
    readonly List<Boostable> boosted = new List<Boostable>();
    AuraData auraData;

    protected override void Awake() {
      base.Awake();
      auraData = buildingData as AuraData;
      rangeVisuals.transform.localScale = new Vector3(Range * 2, Range * 2, 1f);
    }
    protected override IEnumerable<UICommand> CreateUICommands() {
      foreach (ChangeAuraUICommandData data in changeAuraUICommandData) {
        yield return new ChangeAuraUICommand(aura: this, data, currentAuraLevel: 1);
      }

      foreach (UICommand command in base.CreateUICommands()) {
        yield return command;
      }
    }

    public bool CanAfford(float energy) {
      return CurrentEnergy >= energy;
    }

    public void ChangeAura(BoostType boostType) {
      foreach (Boostable boostable in boosted.Where(b => b as UnityEngine.Object != null)) {
        boostable.StopBoosting(BoostType);
        boostable.StartBoosting(boostType, BoostMultiplier);
      }

      BoostType = boostType;
      InvokeDataChange(new Dictionary<DataType, object>() {
        { DataType.Aura, BoostType }
      });
    }

    public void StoreEnergy(float energy) {
      CurrentEnergy += energy;
      if (CurrentEnergy > MaxEnergy) {
        CurrentEnergy = MaxEnergy;
      }

      InvokeDataChange(new Dictionary<DataType, object>() {
        { DataType.CurrentEnergy, CurrentEnergy },
        { DataType.MaxEnergy, MaxEnergy }
      });
    }

    public void UseEnergy(float energy) {
      CurrentEnergy -= energy;
      if (CurrentEnergy < 0) {
        CurrentEnergy = 0;
      }

      InvokeDataChange(new Dictionary<DataType, object>() {
        { DataType.CurrentEnergy, CurrentEnergy },
        { DataType.MaxEnergy, MaxEnergy }
      });
    }

    public override void Select() {
      base.Select();
      rangeVisuals.SetActive(true);
    }

    public override void Deselect() {
      base.Deselect();
      rangeVisuals.SetActive(false);
    }

    void OnEnable() {
      onTick.OnEventInvoked += Boost;
    }

    void OnDisable() {
      onTick.OnEventInvoked -= Boost;
    }

    void Boost(object sender, EventArgs e) {
      foreach (Boostable boostable in boosted.Where(b => b as UnityEngine.Object != null)) {
        boostable.StopBoosting(BoostType);
      }

      boosted.Clear();
      List<Boostable> boostablesInRange = GetBoostableDevicesInRange();
      foreach (Boostable boostable in boostablesInRange.Where(b => b as UnityEngine.Object != null)) {
        if (CanAfford(EnergyPerDevice) == false) {
          return;
        }

        if (boostable.IsBoosted(BoostType) == false) {
          UseEnergy(EnergyPerDevice);
          boostable.StartBoosting(BoostType, BoostMultiplier);
          boosted.Add(boostable);
        }
      }
    }

    List<Boostable> GetBoostableDevicesInRange() {
      Collider[] collidersInRange = Physics.OverlapSphere(Position, Range);
      List<Boostable> devicesInRange = new List<Boostable>();
      foreach (Collider collider in collidersInRange) {
        if (collider.TryGetComponent(out Boostable device)) {
          devicesInRange.Add(device);
        }
      }

      return devicesInRange;
    }

    public override void Demolish() {
      foreach (Boostable boostable in boosted) {
        boostable.StopBoosting(BoostType);
      }

      base.Demolish();
    }

    public override Dictionary<DataType, object> GetData() {
      Dictionary<DataType, object> data = base.GetData();
      data.Add(DataType.MaxEnergy, MaxEnergy);
      data.Add(DataType.CurrentEnergy, CurrentEnergy);
      data.Add(DataType.Aura, BoostType);
      data.Add(DataType.EnergyUsagePerSecond, auraData.energyPerDevicePerSecond * boosted.Count);
      return data;
    }

    protected override Dictionary<DataType, object> GetDataTypesOnUpgrage() {
      Dictionary<DataType, object> data = base.GetDataTypesOnUpgrage();
      data.Add(DataType.MaxEnergy, MaxEnergy);
      data.Add(DataType.CurrentEnergy, CurrentEnergy);
      data.Add(DataType.EnergyUsagePerSecond, auraData.energyPerDevicePerSecond * boosted.Count);
      return data;
    }
  }
}
