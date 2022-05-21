using System;
using System.Collections.Generic;
using UnityEngine;

namespace bts {
  public class Generator : Building {
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
      energyPerTick = EnergyPerSecond / TimeTicker.ticksPerSecond;
      rangeVisuals.transform.localScale = new Vector3(Range * 2, Range * 2, 1f);
      rangeVisuals.SetActive(false);
    }

    List<ElectricDevice> GetDevicesInRange() {
      Collider[] collidersInRange = Physics.OverlapSphere(Center.position, Range);
      List<ElectricDevice> devicesInRange = new List<ElectricDevice>();
      foreach (Collider collider in collidersInRange) {
        if (collider.TryGetComponent(out ElectricDevice device)) {
          devicesInRange.Add(device);
        }
      }

      return devicesInRange;
    }

    void OnEnable() {
      TimeTicker.OnTick += Generate;
    }

    void OnDisable() {
      TimeTicker.OnTick -= Generate;
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
            devices[i].Store(energyPerDevice);
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
  }
}
