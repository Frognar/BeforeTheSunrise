using System.Collections.Generic;
using fro.States;
using UnityEngine;

namespace bts {
  public class Cannon : Building, ElectricDevice, Boostable, CommandReceiver {
    [field: SerializeField] public ElectricArcEventChannel VFXEventChannel { get; private set; }
    [field: SerializeField] public Transform ArcBegin { get; private set; }
    [field: SerializeField] public SFXEventChannel SFXEventChannel { get; private set; }
    public ElectricArcVFXConfiguration ElectricArcConfig => data.electricArcConfig;
    public AudioConfiguration AudioConfig => data.audioConfig;
    public AudioClipsGroup AttackSFX => data.attackSFX;
    float BaseDamage => data.damage * Mathf.Pow(2, BuildingLevel);
    public float Damage => boosts.ContainsKey(BoostType.Damage) ? BaseDamage * boosts[BoostType.Damage] : BaseDamage;
    public float EnergyPerAttack => data.energyPerAttack * Mathf.Pow(1.5f, BuildingLevel);
    public float Range => boosts.ContainsKey(BoostType.Range) ? data.range * boosts[BoostType.Range] : data.range;
    public float MaxEnergy => data.maxEnergy * (int)Mathf.Pow(2, BuildingLevel);
    public float TimeBetweenAttacks => boosts.ContainsKey(BoostType.AttackSpeed) ? data.timeBetweenAttacks / boosts[BoostType.AttackSpeed] : data.timeBetweenAttacks;
    CannonData data;
    public float CurrentEnergy { get; private set; }
    public bool IsOrderedToStop { get; set; }
    public bool IsOrderedToAttack { get; set; }

    public Damageable Target { get; set; }
    public bool IsSelected { get; private set; }
    public bool IsIdle { get; set; }

    [SerializeField] GameObject rangeVisuals;
    StateMachine<Cannon> stateMachine;

    readonly Dictionary<BoostType, float> boosts = new Dictionary<BoostType, float>();

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

      InvokeDataChange(new Dictionary<DataType, object>() {
        { DataType.CurrentEnergy, CurrentEnergy },
        { DataType.MaxEnergy, MaxEnergy }
      });
    }

    public void StoreEnergy(float energy) {
      CurrentEnergy += energy;
      if (CurrentEnergy > MaxEnergy) {
        CurrentEnergy = MaxEnergy;
      }

      InvokeDataChange(new Dictionary<DataType, object>() {
        { DataType.CurrentEnergy, CurrentEnergy },
        { DataType.MaxEnergy, MaxEnergy }
      });
    }

    public override bool IsSameAs(Selectable other) {
      return other is Cannon;
    }

    public bool IsFree() {
      return IsIdle;
    }

    public override Dictionary<DataType, object> GetData() {
      Dictionary<DataType, object> data = base.GetData();
      data.Add(DataType.MaxEnergy, MaxEnergy);
      data.Add(DataType.CurrentEnergy, CurrentEnergy);
      data.Add(DataType.DamagePerSecond, Damage / TimeBetweenAttacks);
      data.Add(DataType.EnergyUsagePerSecond, EnergyPerAttack / TimeBetweenAttacks);
      return data;
    }

    protected override Dictionary<DataType, object> GetDataTypesOnUpgrage() {
      Dictionary<DataType, object> data = base.GetDataTypesOnUpgrage();
      data.Add(DataType.MaxEnergy, MaxEnergy);
      data.Add(DataType.CurrentEnergy, CurrentEnergy);
      data.Add(DataType.DamagePerSecond, Damage / TimeBetweenAttacks);
      data.Add(DataType.EnergyUsagePerSecond, EnergyPerAttack / TimeBetweenAttacks);
      return data;
    }

    public bool IsBoosted(BoostType boostType) {
      return boosts.ContainsKey(boostType);
    }

    public void StartBoosting(BoostType boostType, float multiplier) {
      boosts[boostType] = multiplier;
      rangeVisuals.transform.localScale = new Vector3(Range * 2, Range * 2, 1f);

      InvokeDataChange(new Dictionary<DataType, object>() {
        { DataType.DamagePerSecond, Damage / TimeBetweenAttacks },
        { DataType.EnergyUsagePerSecond, EnergyPerAttack / TimeBetweenAttacks }
      });
    }

    public void StopBoosting(BoostType boostType) {
      _ = boosts.Remove(boostType);
      rangeVisuals.transform.localScale = new Vector3(Range * 2, Range * 2, 1f);
    }
  }
}
