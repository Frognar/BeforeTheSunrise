using System;
using System.Collections.Generic;
using UnityEngine;

namespace bts {
  public class Generator : PlacedObject {
    [SerializeField] GemstoneStorage storage;
    [SerializeField] DemolishUICommandData demolishUICommandData;
    [SerializeField] float energyPerSecond;
    float energyPerTick;
    public float Range => (placedObjectType.customData as GeneratorData).range;
    GameObject rangeVisuals;

    protected override void Start() {
      base.Start();
      UICommands = new List<UICommand>() { new DemolishUICommand(demolishUICommandData, this, storage) };
      energyPerTick = energyPerSecond / TimeTicker.ticksPerSecond;
      rangeVisuals = transform.Find("Range").gameObject;
      rangeVisuals.transform.localScale = new Vector3(Range * 2, Range * 2, 1f);
    }

    List<ElectricDevice> GetDevicesInRange() {
      Collider[] collidersInRange = Physics.OverlapSphere(center.position, Range);
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

    public override void Select() {
      Selected.SetActive(true);
      rangeVisuals.SetActive(true);
    }

    public override void Deselect() {
      Selected.SetActive(false);
      rangeVisuals.SetActive(false);
    }
  }
}
