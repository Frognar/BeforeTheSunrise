using System.Collections.Generic;
using UnityEngine;

namespace bts {
  public abstract class Building : PlacedObject, Damageable, Selectable {
    [SerializeField] DemolishUICommandData demolishUICommandData;
    public virtual IEnumerable<UICommand> UICommands { get; protected set; }
    public GemstoneDictionary BuildingCosts => buildingData.buildingCosts;

    public Vector3 Position => Center.position;
    public bool IsDead => health.CurrentHealth == 0;

    [SerializeField] WorldHealthBar bar;
    Player selector;
    Health health;
    protected CustomBuildingData buildingData;

    protected override void Start() {
      base.Start();
      selector = FindObjectOfType<Player>();
      buildingData = PlaceableObjectType.customData as CustomBuildingData;
      health = new Health(buildingData.healthAmount);
      bar.SetUp(health);
      UICommands = new List<UICommand>() { new DemolishUICommand(demolishUICommandData, this) };
    }

    public void TakeDamage(int amount) {
      health.Damage(amount);
      if (IsDead) {
        GridBuildingSystem.Demolish(transform.position);
      }
    }

    public void DestroySelf() {
      GridBuildingSystem.Demolish(Position);
    }

    public override void Demolish() {
      if (Selected.activeSelf) {
        selector.Deselect(this);
      }

      base.Demolish();
    }
  }
}