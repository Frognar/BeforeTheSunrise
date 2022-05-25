using UnityEngine;

namespace bts {
  public class Obstacle : PlacedObject, Damageable {
    public Vector3 Position => Center.position;
    public bool IsDead => health.CurrentHealth == 0;
    public Bounds Bounds => obstacleCollider.bounds;
    Collider obstacleCollider;
    [SerializeField] WorldHealthBar bar;
    Health health;

    protected override void Start() {
      base.Start();
      obstacleCollider = GetComponent<Collider>();
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
