using System;
using UnityEngine;

namespace bts {
  public class Healer : Building, ElectricDevice, CommandReceiver {
    [field: SerializeField] public LaserEventChannel LaserEventChannel { get; private set; }
    [field: SerializeField] public SFXEventChannel SFXEventChannel { get; private set; }
    [SerializeField] VoidEventChannel onTick;
    [SerializeField] GameObject rangeVisuals;
    [field: SerializeField] public Transform LaserBegining { get; private set; }
    HealerData data;
    public float Range => data.range;
    public float EnergyPerHeal => data.energyPerHeal;
    public float MaxEnergy => data.maxEnergy;
    public float HealAmount => data.healAmount;
    public AudioConfiguration AudioConfig => data.audioConfig;
    public AudioClipsGroup HealSFX => data.healSFX;
    public LaserConfiguration LaserConfig => data.laserConfig;
    public SoundEmitter SoundEmitter { get; set; }
    public Laser Laser { get; set; }
    public Damageable Target { get; set; }
    public bool IsOrderedToStop { get; set; }
    public bool IsOrderedToHeal { get; set; }
    public bool IsSelected { get; private set; }
    public bool IsIdle { get; set; }
    HealerStateMachine stateMachine;

    protected override void Awake() {
      base.Awake();
      data = buildingData as HealerData;
      rangeVisuals.transform.localScale = new Vector3(Range * 2, Range * 2, 1f);
      rangeVisuals.SetActive(false);
      stateMachine = new HealerStateMachine(this);
    }

    protected override void Start() {
      base.Start();
      stateMachine.Start();
    }

    void OnEnable() {
      onTick.OnEventInvoked += UpdateStateMachine;
    }

    void OnDisable() {
      onTick.OnEventInvoked -= UpdateStateMachine;
    }

    public override void Select() {
      base.Select();
      rangeVisuals.SetActive(true);
      IsSelected = true;
    }

    public override void Deselect() {
      base.Deselect();
      rangeVisuals.SetActive(false);
      IsSelected = false;
    }

    void UpdateStateMachine(object sender, EventArgs args) {
      stateMachine.Update();
    }

    public float CurrentEnergy { get; private set; }

    public bool CanAfford(float energy) {
      return CurrentEnergy >= energy;
    }

    public void UseEnergy(float energy) {
      CurrentEnergy -= energy;
      if (CurrentEnergy < 0) {
        CurrentEnergy = 0;
      }
    }

    public void StoreEnergy(float energy) {
      CurrentEnergy += energy;
      if (CurrentEnergy > MaxEnergy) {
        CurrentEnergy = MaxEnergy;
      }
    }

    public override bool IsSameAs(Selectable other) {
      return other is Healer;
    }

    public bool IsFree() {
      return IsIdle;
    }
  }
}
