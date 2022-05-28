using UnityEngine;

namespace bts {
  public class Obstacle : PlacedObject, Damageable {
    public Vector3 Position => Center.position;
    public bool IsDead => health.CurrentHealth == 0;
    public Bounds Bounds => Obstacle.bounds;
    [SerializeField] WorldHealthBar bar;
    Health health;

    protected override void Start() {
      base.Start();
      health = new Health(10);
      bar.SetUp(health);
    }

    public void TakeDamage(int amount) {
      health.Damage(amount);
      if (IsDead) {
        GridBuildingSystem.Demolish(transform.position);
      }
    }
  }
}
