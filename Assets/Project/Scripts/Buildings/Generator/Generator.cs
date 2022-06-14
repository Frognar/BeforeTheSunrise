using System;
using System.Collections.Generic;
using fro.ValueAssets;
using UnityEngine;

namespace bts {
  public class Generator : Building {
    [SerializeField] SFXEventChannel sfxEventChannel;
    [SerializeField] ElectricArcEventChannel vfxEventChannel;
    [SerializeField] Transform arcBegin;
    [SerializeField] IntAsset ticksPerSecond;
    [SerializeField] VoidEventChannel onTick;
    [SerializeField] GameObject rangeVisuals;
    public float Range => data.range;
    float EnergyPerSecond => data.energyPerSecond * Mathf.Pow(2, BuildingLevel);
    int MaxDevices => data.maxDevices;
    GeneratorData data;
    float EnergyPerTick => EnergyPerSecond / ticksPerSecond;
    SoundEmitter soundEmitter;
    readonly ElectricArcParameters parameters = new ElectricArcParameters();

    protected override void Awake() {
      base.Awake();
      data = buildingData as GeneratorData;
      rangeVisuals.transform.localScale = new Vector3(Range * 2, Range * 2, 1f);
      rangeVisuals.SetActive(false);
    }

    List<ElectricDevice> GetDevicesInRange() {
      Collider[] collidersInRange = Physics.OverlapSphere(Position, Range);
      List<ElectricDevice> devicesInRange = new List<ElectricDevice>();
      foreach (Collider collider in collidersInRange) {
        if (collider.TryGetComponent(out ElectricDevice device)) {
          devicesInRange.Add(device);
        }
      }

      return devicesInRange;
    }

    void OnEnable() {
      onTick.OnEventInvoked += Generate;
    }

    void OnDisable() {
      onTick.OnEventInvoked -= Generate;
    }

    protected override void OnDestroy() {
      base.OnDestroy();
      onTick.OnEventInvoked -= Generate;
    }

    void Generate(object s, EventArgs e) {
      List<ElectricDevice> devices = GetDevicesInRange();
      if (devices.Count > 0) {
        int devicesCount = devices.Count > MaxDevices ? MaxDevices : devices.Count;
        float energyPerDevice = EnergyPerTick / devicesCount;
        int offset = 0;
        for (int i = 0; i < devicesCount + offset && i < devices.Count; i++) {
          if (devices[i].IsFull) {
            offset++;
          }
          else {
            devices[i].StoreEnergy(energyPerDevice);
            parameters.Source = arcBegin;
            parameters.TargetPosition = devices[i].Center.position;
            vfxEventChannel.RaiseSpawnEvent(data.electricArcConfig, parameters);
            StartSFX();
          }
        }

        bool allDevicesAreFull = offset == devices.Count;
        if (allDevicesAreFull) {
          StopSFX();
        }
      }
      else {
        StopSFX();
      }
    }

    void StartSFX() {
      if (soundEmitter == null) {
        soundEmitter = sfxEventChannel.RaisePlayEventWithEmitter(data.generateSFX, data.audioConfig, Position);
      }
    }

    void StopSFX() {
      if (soundEmitter != null) {
        soundEmitter.Stop();
        soundEmitter = null;
      }
    }

    public void OnDemolish() {
      StopSFX();
    }

    public override void Select() {
      base.Select();
      rangeVisuals.SetActive(true);
    }

    public override void Deselect() {
      base.Deselect();
      rangeVisuals.SetActive(false);
    }

    public override bool IsSameAs(Selectable other) {
      return other is Generator;
    }

    public override Dictionary<DataType, object> GetData() {
      Dictionary<DataType, object> data = base.GetData();
      data.Add(DataType.EnergyPerSecond, EnergyPerSecond);
      return data;
    }

    protected override Dictionary<DataType, object> GetDataTypesOnUpgrage() {
      Dictionary<DataType, object> data = base.GetDataTypesOnUpgrage();
      data.Add(DataType.EnergyPerSecond, EnergyPerSecond);
      return data;
    }
  }
}
