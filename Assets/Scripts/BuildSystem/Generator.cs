using System;
using System.Collections.Generic;
using UnityEngine;

namespace bts {
  public class Generator : PlacedObject {
    [SerializeField] float energyPerSecond;
    float energyPerTick;
    [SerializeField] float range;

    protected override void Start() {
      base.Start();
      energyPerTick = energyPerSecond / TimeTicker.ticksPerSecond;
    }

    List<ElectricDevice> GetDevicesInRange() {
      Collider[] collidersInRange = Physics.OverlapSphere(center.position, range);
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
      int devicesCount = devices.Count;
      float energyPerDevice = energyPerTick / devicesCount;
      foreach (ElectricDevice device in devices) {
        device.Store(energyPerDevice);
      }
    }
  }
}
