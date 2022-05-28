using System;
using UnityEngine;

namespace bts {
  public class Obstacle : PlacedObject, Damageable {
    [SerializeField] GemstoneStorage storage;
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
        if (UnityEngine.Random.value < 0.3f) {
          Array values = Enum.GetValues(typeof(GemstoneType));
          GemstoneType type = (GemstoneType)values.GetValue(UnityEngine.Random.Range(0, values.Length));
          int gemstoneAmount = UnityEngine.Random.Range(1, 5);
          storage.Store(type, gemstoneAmount);
        }
      }
    }
  }
}
