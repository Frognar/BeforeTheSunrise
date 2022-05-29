using System.Collections.Generic;
using UnityEngine;

namespace bts {
  public abstract class Building : PlacedObject, Damageable, Selectable {
    [SerializeField] DemolishUICommandData demolishUICommandData;
    public virtual IEnumerable<UICommand> UICommands { get; protected set; }
    public GemstoneDictionary BuildingCosts => buildingData.buildingCosts;
    public Vector3 Position => Center.position;
    public bool IsDead => health.HasNoHealth;
    public Bounds Bounds => Obstacle.bounds;

    [SerializeField] WorldHealthBar bar;
    Health health;
    protected CustomBuildingData buildingData;

    protected override void Awake() {
      base.Awake();
      buildingData = PlaceableObjectType.customData as CustomBuildingData;
      health = new Health(buildingData.healthAmount);
      bar.SetUp(health);
      UICommands = new List<UICommand>() { new DemolishUICommand(demolishUICommandData, this) };
    }

    public void TakeDamage(int amount) {
      health.Damage(amount);
      if (IsDead) {
        GridBuildingSystem.Demolish(Position);
      }
    }

    public void DestroySelf() {
      GridBuildingSystem.Demolish(Position);
    }
    
    public override Dictionary<string, object> GetData() {
      return new Dictionary<string, object>() {
        { "Health", bar.Health }
      };
    }
  }
}
