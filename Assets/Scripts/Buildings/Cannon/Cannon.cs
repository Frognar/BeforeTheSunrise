using UnityEngine;

namespace bts {
  public class Cannon : Building, ElectricDevice, CommandReceiver {
    [field: SerializeField] public VFXEventChannel VFXEventChannel { get; private set; }
    [field: SerializeField] public Transform ArcBegin { get; private set; }
    [field: SerializeField] public SFXEventChannel SFXEventChannel { get; private set; }
    public ElectricArcVFXConfiguration ElectricArcConfig => data.electricArcConfig;
    public AudioConfiguration AudioConfig => data.audioConfig;
    public AudioClipsGroup AttackSFX => data.attackSFX;
    public int Damage => data.damage;
    public float EnergyPerAttack => data.energyPerAttack;
    public float Range => data.range;
    public float MaxEnergy => data.maxEnergy;
    public float TimeBetweenAttacks => data.timeBetweenAttacks;
    CannonData data;
    public float CurrentEnergy { get; private set; }
    public bool IsOrderedToStop { get; set; }
    public bool IsOrderedToAttack { get; set; }

    public Damageable Target { get; set; }
    public bool IsSelected { get; private set; }
    public bool IsIdle { get; set; }
    
    [SerializeField] GameObject rangeVisuals;
    StateMachine<Cannon> stateMachine;

    protected override void Awake() {
      base.Awake();
      data = buildingData as CannonData;
      rangeVisuals.transform.localScale = new Vector3(Range * 2, Range * 2, 1f);
      rangeVisuals.SetActive(false);
      stateMachine = new CannonStateMachine(this);
    }

    protected override void Start() {
      base.Start();
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

    public bool IsFree() {
      return IsIdle;
    }
  }
}
