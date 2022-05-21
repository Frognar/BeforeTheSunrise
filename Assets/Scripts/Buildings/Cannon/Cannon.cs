using UnityEngine;

namespace bts {
  public class Cannon : Building, ElectricDevice {
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

    CannonStateFactory stateFactory;
    public CannonBaseState CurrentState { get; private set; }
    public bool IsSelected { get; private set; }
    public bool IsIdle { get; set; }
    
    [SerializeField] GameObject rangeVisuals;
    bool DataLoaded => data != null;

    protected override void Start() {
      base.Start();
      data = buildingData as CannonData;
      rangeVisuals.transform.localScale = new Vector3(Range * 2, Range * 2, 1f);
      rangeVisuals.SetActive(false);
      stateFactory = new CannonStateFactory(context: this);
      SwitchState(stateFactory.Idle);
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

    public void SwitchState(CannonBaseState state) {
      CurrentState = state;
      CurrentState.EnterState();
    }

    void Update() {
      CurrentState.UpdateState();
    }

    public void Use(float energy) {
      CurrentEnergy -= energy;
      if (CurrentEnergy < 0) {
        CurrentEnergy = 0;
      }
    }

    public void Store(float energy) {
      CurrentEnergy += energy;
      if (CurrentEnergy > MaxEnergy) {
        CurrentEnergy = MaxEnergy;
      }
    }
  }
}
