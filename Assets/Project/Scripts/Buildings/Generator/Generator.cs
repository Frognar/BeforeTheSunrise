using System;
using System.Collections.Generic;
using bts.Gemstones;
using fro.ValueAssets;
using UnityEngine;

namespace bts {
  [RequireComponent(typeof(GeneratorComponent))]
  public class Generator : Building {
    [Header("Tick")]
    [SerializeField] VoidEventChannel onTick;
    [SerializeField] IntAsset ticksPerSecond;
    
    [Header("SFX")]
    [SerializeField] AudioRequester audioRequester;
    [SerializeField] AudioClipsGroup generateSFX;

    [Header("Visuals")]
    [SerializeField] GameObject rangeVisuals;
    [SerializeField] ElectricArcRequester electricArc;

    [Space]
    [SerializeField] GeneratorComponent generator;
    GeneratorData data;
    float EnergyPerSecond => data.EnergyPerSecond * Mathf.Pow(2, BuildingLevel);
    float EnergyPerTick => EnergyPerSecond / ticksPerSecond;

    protected override void Awake() {
      base.Awake();
      data = buildingData as GeneratorData;
      generator.SetUp(data.Range, EnergyPerTick, data.MaxDevices);
      rangeVisuals.transform.localScale = new Vector3(data.Range * 2, data.Range * 2, 1f);
      rangeVisuals.SetActive(false);
    }

    public override void Upgrgade(GemstoneDictionary cost) {
      base.Upgrgade(cost);
      generator.SetUp(data.Range, EnergyPerTick, data.MaxDevices);
    }

    void OnEnable() {
      onTick.OnEventInvoked += Generate;
      generator.OnStartCharging += StartSound;
      generator.OnStopCharging += StopSound;
      generator.OnCharge += CreateElectricArc;
    }

    void OnDisable() {
      onTick.OnEventInvoked -= Generate;
      generator.OnStartCharging -= StartSound;
      generator.OnStopCharging -= StopSound;
      generator.OnCharge -= CreateElectricArc;
    }

    void Generate(object s, EventArgs e) {
      generator.Generate();
    }

    void CreateElectricArc(object sender, GeneratorComponent.GeneratorEventArgs args) {
      electricArc.Create(args.DevicePosition);
    }

    void StopSound(object sender, EventArgs e) {
      audioRequester.StopSFX();
    }

    void StartSound(object sender, EventArgs e) {
      audioRequester.StartSFX(generateSFX, Position);
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
      return other is Generator g && BuildingLevel == g.BuildingLevel;
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
