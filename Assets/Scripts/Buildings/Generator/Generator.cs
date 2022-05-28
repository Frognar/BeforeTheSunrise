using System;
using System.Collections.Generic;
using UnityEngine;

namespace bts {
  public class Generator : Building {
    [SerializeField] VFXEventChannel vfxEventChannel;
    [SerializeField] Transform arcBegin;
    [SerializeField] Vector3Asset arcColor;
    [SerializeField] IntAsset ticksPerSecond;
    [SerializeField] VoidEventChannel onTick;
    [SerializeField] GameObject rangeVisuals;
    public float Range => DataLoaded ? data.range : 0;
    float EnergyPerSecond => DataLoaded ? data.energyPerSecond : 0;
    int MaxDevices => DataLoaded ? data.maxDevices : 0;
    GeneratorData data;
    float energyPerTick;
    bool DataLoaded => data != null;

    protected override void Start() {
      base.Start();
      data = buildingData as GeneratorData;
      energyPerTick = EnergyPerSecond / ticksPerSecond;
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
        float energyPerDevice = energyPerTick / devicesCount;
        int offset = 0;
        for (int i = 0; i < devicesCount + offset && i < devices.Count; i++) {
          if (devices[i].IsFull) {
            offset++;
          }
          else {
            devices[i].StoreEnergy(energyPerDevice);
            vfxEventChannel.RaiseVFXEvent(arcBegin, devices[i].Center, arcColor, duration: 0.5f);
          }
        }
      }
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
  }
}
