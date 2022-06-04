using UnityEngine;

namespace bts {
  public class Aura : Building, ElectricDevice {
    [SerializeField] GameObject rangeVisuals;
    public float Range => data.range;
    public float MaxEnergy => data.maxEnergy;
    public float EnergyPerDevice => data.energyPerDevice;
    public float CurrentEnergy { get; private set; }
    AuraData data;

    protected override void Awake() {
      base.Awake();
      data = buildingData as AuraData;
      rangeVisuals.transform.localScale = new Vector3(Range * 2, Range * 2, 1f);
    }

      public bool CanAfford(float energy) {
      return CurrentEnergy >= energy;
    }

    public void StoreEnergy(float energy) {
      CurrentEnergy += energy;
      if (CurrentEnergy > MaxEnergy) {
        CurrentEnergy = MaxEnergy;
      }
    }

    public void UseEnergy(float energy) {
      CurrentEnergy -= energy;
      if (CurrentEnergy < 0) {
        CurrentEnergy = 0;
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
