using UnityEngine;

namespace bts {
  public class Cannon : Building, ElectricDevice {
    [field: SerializeField] public SFXEventChannel SFXEventChannel { get; private set; }
    [field: SerializeField] public AudioConfiguration AudioConfig { get; private set; }
    [field: SerializeField] public AudioClip AttackSFX { get; private set; }
    CannonData data;
    public int Damage => DataLoaded ? data.damage : 0;
    public float EnergyPerAttack => DataLoaded ? data.energyPerAttack : 0;
    public float Range => DataLoaded ? data.range : 0;
    public float MaxEnergy => DataLoaded ? data.maxEnergy : 0;
    public float TimeBetweenAttacks => DataLoaded ? data.timeBetweenAttacks : 0;
    public float CurrentEnergy { get; private set; }
    public bool IsOrderedToStop { get; set; }
    public bool IsOrderedToAttack { get; set; }

    public Damageable Target { get; set; }
    public bool IsSelected { get; private set; }
    public bool IsIdle { get; set; }
    
    [SerializeField] GameObject rangeVisuals;
    bool DataLoaded => data != null;

    StateMachine<Cannon> stateMachine;

    protected override void Start() {
      base.Start();
      data = buildingData as CannonData;
      rangeVisuals.transform.localScale = new Vector3(Range * 2, Range * 2, 1f);
      rangeVisuals.SetActive(false);
      stateMachine = new CannonStateMachine(this);
      stateMachine.Start();
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

    void Update() {
      stateMachine.Update();
    }

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
      return other is Cannon;
    }
  }
}
