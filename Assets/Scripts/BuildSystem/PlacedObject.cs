using UnityEngine;

namespace bts {
  public class PlacedObject : Placeable, Selectable, Damageable {
    public string Name => placedObjectType.name;
    public Transform Transform => center;
    public Affiliation ObjectAffiliation => placedObjectType.objectAffiliation;
    public Type ObjectType => placedObjectType.objectType;
    public GameObject Selected { get; private set; }
    public Vector3 Position => center.position;
    public bool IsDead => health.CurrentHealth == 0;

    Health health;
    WorldHealthBar bar;

    void Start() {
      health = new Health(10);
      Selected = transform.Find("Selected").gameObject;
      bar = GetComponentInChildren<WorldHealthBar>();
      bar.SetUp(health);
    }

    public void TakeDamage(int amount) {
      health.Damage(amount);
      if (health.CurrentHealth == 0) {
        gridBuildingSystem.Demolish(transform.position);
      }
    }
  }
}
