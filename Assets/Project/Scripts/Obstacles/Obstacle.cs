using System.Collections.Generic;
using UnityEngine;

namespace bts {
  public class Obstacle : PlacedObject, Damageable {
    [SerializeField][Range(0, 1)] float addResourceChance = 0.3f;
    [SerializeField] GemstoneStorage storage;
    [SerializeField] SFXEventChannel sfxEventChannel;
    [SerializeField] AudioClipsGroup destroySFX;
    [SerializeField] AudioConfiguration audioConfiguration;
    [SerializeField] DestroyEventChannel destroyEventChannel;
    [SerializeField] DestroyConfiguration destroyConfiguration;
    readonly DestroyParameters destroyParmaeters = new DestroyParameters();
    public Vector3 Position => Center.position;
    public bool IsDead => health.HasNoHealth;
    public bool IsIntact => health.HasFullHealth;
    public Bounds Bounds => Obstacle.bounds;
    [SerializeField] WorldHealthBar bar;
    Health health;

    protected override void Start() {
      base.Start();
      health = new Health(10);
      bar.SetUp(health);
    }

    public void TakeDamage(float amount) {
      health.Damage(amount);
      if (IsDead) {
        GridBuildingSystem.Demolish(transform.position);
        if (Random.value < addResourceChance) {
          storage.StoreRandom(Random.Range(2, 8));
        }
      }
    }

    public void Heal(float amount) {
      health.Heal(amount);
    }

    public override Dictionary<string, object> GetData() {
      return new Dictionary<string, object>() {
        { "Health", bar.Health }
      };
    }

    public override void Demolish() {
      sfxEventChannel.RaisePlayEvent(destroySFX, audioConfiguration, Position);
      destroyParmaeters.Position = Position;
      destroyEventChannel.RaiseVFXEvent(destroyConfiguration, destroyParmaeters);
      base.Demolish();
    }
  }
}
