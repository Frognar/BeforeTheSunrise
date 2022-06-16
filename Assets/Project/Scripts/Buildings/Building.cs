using System;
using System.Collections.Generic;
using bts.Gemstones;
using fro.HealthSystem;
using UnityEngine;

namespace bts {
  public abstract class Building : MonoBehaviour, Damageable, Selectable, Loggable {
    public event Action<Dictionary<DataType, object>> OnDataChange = delegate { };
    public void InvokeDataChange(Dictionary<DataType, object> data) {
      OnDataChange.Invoke(data);
    }

    [SerializeField] int maxLevel = 10;
    [SerializeField] protected GemstoneStorage storage;
    [SerializeField] BuildingRegister register;

    [field: SerializeField] public string Name { get; private set; }
    public Affiliation ObjectAffiliation => Affiliation.Player;
    public Type ObjectType => Type.Building;
    [field: SerializeField] public GameObject Selected { get; private set; }
    [field: SerializeField] public Transform Center { get; private set; }
    
    [SerializeField] UpgradeBuildingUICommandData upgradeBuildingUICommandData;
    [SerializeField] DemolishUICommandData demolishUICommandData;
    public virtual IEnumerable<UICommand> UICommands { get; protected set; }
    public GemstoneDictionary BuildingCosts => CalculateTotalCost();
    public Vector3 Position => Center.position;
    public bool IsDead => health.IsDead;
    public bool IsIntact => health.IsInFullHealth;
    [SerializeField] Collider buildingCollider;
    public Bounds Bounds => buildingCollider.bounds;

    public int BuildingLevel { get; private set; }
    public Sprite Icon { get; }
    [field: SerializeField] public SelectablesEventChannel SelectablesEventChannel { get; private set; }

    [SerializeField] WorldHealthBar bar;
    Health health;
    [SerializeField] protected CustomBuildingData buildingData;

    protected virtual void Awake() {
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
      Dictionary<DataType, object> data = GetHealthData();
      data.Add(DataType.Level, BuildingLevel);
      return data;
    }

    protected Dictionary<DataType, object> GetHealthData() {
      return new Dictionary<DataType, object> {
        { DataType.MaxHealth, health.MaxHealth },
        { DataType.CurrentHealth, bar.Health.CurrentHealth },
      };
    }

    protected virtual void OnDestroy() {
      register.Unregister(this);
      SelectablesEventChannel.Invoke(this);
    }

    protected virtual IEnumerable<UICommand> CreateUICommands() {
      yield return new UpgradeBuildingUICommand(this, upgradeBuildingUICommandData);
      yield return new DemolishUICommand(demolishUICommandData, this);
    }

    public void TakeDamage(float amount) {
      health.Damage(amount);
      InvokeDataChange(GetHealthData());

      if (IsDead) {
        Destroy(gameObject);
      }
    }

    public void Heal(float amount) {
      health.Heal(amount);
      InvokeDataChange(GetHealthData());
    }

    public virtual Dictionary<DataType, object> GetData() {
      return new Dictionary<DataType, object>() {
        { DataType.Name, Name },
        { DataType.MaxHealth, health.MaxHealth },
        { DataType.CurrentHealth, bar.Health.CurrentHealth },
        { DataType.Level, BuildingLevel },
      };
    }

    public virtual void Select() {
      Selected.SetActive(true);
    }

    public virtual void Deselect() {
      Selected.SetActive(false);
    }

    public abstract bool IsSameAs(Selectable other);

    public void DestroySelf() {
      Destroy(gameObject);
    }

    public string GetLogData() {
      return $"{Name}: {BuildingLevel}";
    }
  }
}
