using System;
using System.Collections.Generic;
using UnityEngine;

namespace bts {
  public abstract class Building : PlacedObject, Damageable, Selectable {
    public override event Action<Dictionary<DataType, object>> OnDataChange = delegate { };
    public void InvokeDataChange(Dictionary<DataType, object> data) {
      OnDataChange.Invoke(data);
    }
    
    [SerializeField] DemolishUICommandData demolishUICommandData;
    public virtual IEnumerable<UICommand> UICommands { get; protected set; }
    public GemstoneDictionary BuildingCosts => buildingData.buildingCosts;
    public Vector3 Position => Center.position;
    public bool IsDead => health.HasNoHealth;
    public bool IsIntact => health.HasFullHealth;
    public Bounds Bounds => Obstacle.bounds;

    [SerializeField] WorldHealthBar bar;
    Health health;
    protected CustomBuildingData buildingData;

    protected override void Awake() {
      base.Awake();
      buildingData = PlaceableObjectType.customData as CustomBuildingData;
      health = new Health(buildingData.healthAmount);
      bar.SetUp(health);
      UICommands = CreateUICommands();
    }

    protected virtual IEnumerable<UICommand> CreateUICommands() {
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
        { DataType.CurrentHealth, bar.Health.CurrentHealth }
      });
    }

    public void DestroySelf() {
      GridBuildingSystem.Demolish(Position);
    }

    public override Dictionary<DataType, object> GetData() {
      return new Dictionary<DataType, object>() {
        { DataType.Name, Name },
        { DataType.MaxHealth, health.MaxHealth },
        { DataType.CurrentHealth, bar.Health.CurrentHealth }
      };
    }
  }
}
