using System;
using System.Collections.Generic;
using UnityEngine;

namespace bts {
  public abstract class Building : PlacedObject, Damageable, Selectable {
    [SerializeField] int maxLevel = 10;
    [SerializeField] protected GemstoneStorage storage;
    [SerializeField] BuildingRegister register;
    public override event Action<Dictionary<DataType, object>> OnDataChange = delegate { };
    public void InvokeDataChange(Dictionary<DataType, object> data) {
      OnDataChange.Invoke(data);
    }

    [SerializeField] UpgradeBuildingUICommandData upgradeBuildingUICommandData;
    [SerializeField] DemolishUICommandData demolishUICommandData;
    public virtual IEnumerable<UICommand> UICommands { get; protected set; }
    public GemstoneDictionary BuildingCosts => CalculateTotalCost();
    public Vector3 Position => Center.position;
    public bool IsDead => health.IsDead;
    public bool IsIntact => health.IsInFullHealth;
    public Bounds Bounds => Obstacle.bounds;

    public int BuildingLevel { get; private set; }

    [SerializeField] WorldHealthBar bar;
    Health health;
    protected CustomBuildingData buildingData;

    protected override void Awake() {
      base.Awake();
      buildingData = PlaceableObjectType.customData as CustomBuildingData;
      health = new Health(buildingData.healthAmount);
      bar.SetUp(health);
      UICommands = CreateUICommands();
      register.Register(this);
    }

    protected virtual GemstoneDictionary CalculateTotalCost() {
      return buildingData.buildingCosts;
    }
    
    public virtual bool IsCurrentMaxLevel() {
      return BuildingLevel == maxLevel || BuildingLevel > register.CurrentTechnologyLevel;
    }
    
    public virtual bool CanUpgrade(GemstoneDictionary cost) {
      return IsCurrentMaxLevel() == false && storage.CanAfford(cost);
    }
    
    public virtual void Upgrgade(GemstoneDictionary cost) {
      storage.Discard(cost);
      BuildingLevel++;
      health.ChangeMaxHealth(buildingData.healthAmount * Mathf.Pow(2, BuildingLevel));
      UICommands = CreateUICommands();
      SelectablesEventChannel.InvokeOnRefresh(this);
      OnDataChange.Invoke(GetDataTypesOnUpgrage());
    }

    protected virtual Dictionary<DataType, object> GetDataTypesOnUpgrage() {
      return new Dictionary<DataType, object> {
        { DataType.MaxHealth, health.MaxHealth },
        { DataType.CurrentHealth, bar.Health.CurrentHealth },
        { DataType.Level, BuildingLevel }
      };
    }

    protected override void OnDestroy() {
      base.OnDestroy();
      register.Unregister(this);
    }

    protected virtual IEnumerable<UICommand> CreateUICommands() {
      yield return new UpgradeBuildingUICommand(this, upgradeBuildingUICommandData);
      yield return new DemolishUICommand(demolishUICommandData, this);
    }

    public void TakeDamage(float amount) {
      health.Damage(amount);
      InvokeDataChange(new Dictionary<DataType, object>() {
        { DataType.MaxHealth, health.MaxHealth },
        { DataType.CurrentHealth, bar.Health.CurrentHealth }
      });

      if (IsDead) {
        GridBuildingSystem.Demolish(Position);
      }
    }

    public void Heal(float amount) {
      health.Heal(amount);
      InvokeDataChange(new Dictionary<DataType, object>() {
        { DataType.MaxHealth, health.MaxHealth },
        { DataType.CurrentHealth, bar.Health.CurrentHealth },
      });
    }

    public void DestroySelf() {
      GridBuildingSystem.Demolish(Position);
    }

    public override Dictionary<DataType, object> GetData() {
      return new Dictionary<DataType, object>() {
        { DataType.Name, Name },
        { DataType.MaxHealth, health.MaxHealth },
        { DataType.CurrentHealth, bar.Health.CurrentHealth },
        { DataType.Level, BuildingLevel },
      };
    }
  }
}
