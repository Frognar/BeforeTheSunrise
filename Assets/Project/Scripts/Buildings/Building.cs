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
    public bool IsDead => healthComponent.Health.IsDead;
    public bool IsIntact => healthComponent.Health.IsInFullHealth;
    [SerializeField] Collider buildingCollider;
    public Bounds Bounds => buildingCollider.bounds;

    public int BuildingLevel { get; private set; }
    public Sprite Icon { get; }
    [field: SerializeField] public SelectablesEventChannel SelectablesEventChannel { get; private set; }

    [SerializeField] HealthComponent healthComponent;
    [SerializeField] protected CustomBuildingData buildingData;

    protected virtual void Awake() {
      healthComponent.Init(buildingData.healthAmount);
      healthComponent.Health.OnCurrentHealthChange += OnCurrentHealthChange;
      healthComponent.Health.OnDie += OnDie;
      UICommands = CreateUICommands();
      register.Register(this);
    }

    void OnCurrentHealthChange(object sender, EventArgs e) {
      InvokeDataChange(GetHealthData());
    }

    void OnDie(object sender, EventArgs e) {
      Destroy(gameObject);
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
      healthComponent.ChangeMaxHealth(buildingData.healthAmount * Mathf.Pow(2, BuildingLevel));
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
        { DataType.MaxHealth, healthComponent.GetMaxHealth() },
        { DataType.CurrentHealth, healthComponent.GetCurrentHealth() },
      };
    }

    protected virtual void OnDestroy() {
      healthComponent.Health.OnCurrentHealthChange -= OnCurrentHealthChange;
      healthComponent.Health.OnDie -= OnDie;
      register.Unregister(this);
      SelectablesEventChannel.Invoke(this);
    }

    protected virtual IEnumerable<UICommand> CreateUICommands() {
      if (upgradeBuildingUICommandData != null) {
        yield return new UpgradeBuildingUICommand(this, upgradeBuildingUICommandData);
      }

      yield return new DemolishUICommand(demolishUICommandData, this);
    }

    public void TakeDamage(float amount) {
      healthComponent.Damage(amount);
    }
    
    public void Heal(float amount) {
      healthComponent.Heal(amount);
    }

    public virtual Dictionary<DataType, object> GetData() {
      Dictionary<DataType, object> data = GetHealthData();
      data.Add(DataType.Name, Name);
      data.Add(DataType.Level, BuildingLevel);
      return data;
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
