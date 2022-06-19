using System;
using fro.ValueAssets;
using UnityEngine;

namespace bts {
  [RequireComponent(typeof(GeneratorComponent))]
  public class PortableGenerator : MonoBehaviour {
    [Header("Tick")]
    [SerializeField] VoidEventChannel onTick;
    [SerializeField] IntAsset ticksPerSecond;

    [Header("Visuals")]
    [SerializeField] ElectricArcRequester electricArc;

    [Header("Generator data")]
    [SerializeField][Range(0f, 10f)] float range = 3f;
    [SerializeField][Range(0f, 100f)] float energyPerSecond = 15f;
    [SerializeField][Range(1, 5)] int maxDevices = 3;
    float EnergyPerTick => energyPerSecond / ticksPerSecond;

    GeneratorComponent generator;

    void Awake() {
      generator = GetComponent<GeneratorComponent>();
      generator.SetUp(range, EnergyPerTick, maxDevices);
    }

    void OnEnable() {
      onTick.OnEventInvoked += Generate;
      generator.OnCharge += ShowVisuals;
    }

    void OnDisable() {
      onTick.OnEventInvoked -= Generate;
      generator.OnCharge -= ShowVisuals;
    }
    
    private void ShowVisuals(object sender, GeneratorComponent.GeneratorEventArgs args) {
      electricArc.Create(args.DevicePosition);
    }
    
    void Generate(object s, EventArgs e) {
      generator.Generate();
    }
  }
}
