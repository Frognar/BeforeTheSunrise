using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace bts {
  public class GeneratorComponent : MonoBehaviour {
    public class GeneratorEventArgs : EventArgs {
      public Vector3 DevicePosition { get; }
      public GeneratorEventArgs(Vector3 devicePosition) {
        DevicePosition = devicePosition;
      }
    }

    public event EventHandler<GeneratorEventArgs> OnCharge;
    public event EventHandler OnStartCharging;
    public event EventHandler OnStopCharging;
    [SerializeField] Transform center;
    float range;
    float energyPerTick;
    int maxDevices;

    public void SetUp(float range, float energyPerTick, int maxDevices) {
      this.range = range;
      this.energyPerTick = energyPerTick;
      this.maxDevices = maxDevices;
    }

    public void Generate() {
      List<ElectricDevice> devicesInRange = InRangeFinder.Find<ElectricDevice>(center.position, range);
      List<ElectricDevice> devicedToCharge = devicesInRange.Where(device => device.IsFull == false).Take(maxDevices).ToList();
      if (devicedToCharge.Count > 0) {
        Charge(devicedToCharge);
        OnStartCharging?.Invoke(this, EventArgs.Empty);
      }
      else {
        OnStopCharging?.Invoke(this, EventArgs.Empty);
      }
    }

    void Charge(List<ElectricDevice> devicesToCharge) {
      float totalEnergyLacks = devicesToCharge.Sum(device => device.MaxEnergy - device.CurrentEnergy);
      bool canChargeAll = totalEnergyLacks <= energyPerTick;
      if (canChargeAll) {
        foreach (ElectricDevice device in devicesToCharge) {
          Charge(device, device.MaxEnergy - device.CurrentEnergy);
        }
      }
      else {
        foreach (ElectricDevice device in devicesToCharge) {
          float scale = (device.MaxEnergy - device.CurrentEnergy) / totalEnergyLacks;
          Charge(device, energyPerTick * scale);
        }
      }
    }
    
    void Charge(ElectricDevice device, float energy) {
      device.StoreEnergy(energy);
      OnCharge?.Invoke(this, new GeneratorEventArgs(device.Center.position));
    }
  }
}
