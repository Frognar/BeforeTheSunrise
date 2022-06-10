using System;
using System.Collections.Generic;
using UnityEngine;

namespace bts {
  public class Obstacle : PlacedObject, Damageable {
    [SerializeField] PopupTextEventChannel popupTextEventChannel;
    public override event Action<Dictionary<DataType, object>> OnDataChange = delegate { };
    [SerializeField][Range(0, 1)] float addResourceChance = 0.5f;
    [SerializeField] GemstoneStorage storage;
    [SerializeField] SFXEventChannel sfxEventChannel;
    [SerializeField] AudioClipsGroup destroySFX;
    [SerializeField] AudioConfiguration audioConfiguration;
    [SerializeField] DestroyEventChannel destroyEventChannel;
    [SerializeField] DestroyConfiguration destroyConfiguration;
    readonly DestroyParameters destroyParmaeters = new DestroyParameters();
    public Vector3 Position => Center.position;
    public bool IsDead => Health.IsDead;
    public bool IsIntact => Health.IsInFullHealth;
    public Bounds Bounds => Obstacle.bounds;
    Health Health => healthComponent.Health;
    [SerializeField] HealthComponent healthComponent;

    protected override void Start() {
      base.Start();
      healthComponent.Init(10f);
    }

    public void TakeDamage(float amount) {
      Health.Damage(amount);
      OnDataChange.Invoke(new Dictionary<DataType, object>() {
        { DataType.MaxHealth, Health.MaxHealth },
        { DataType.CurrentHealth, Health.CurrentHealth }
      });

      if (IsDead) {
        if (UnityEngine.Random.value < addResourceChance) {
          GemstoneType type = storage.GetRandomType();
          int count = UnityEngine.Random.Range(2, 6);
          storage.Store(type, count);
          PopupTextParameters popupParams = new PopupTextParameters() {
            Position = Center.position,
            GemstoneType = type,
            Text = $"+{count}"
          };

          popupTextEventChannel.RaiseSpawnEvent(PopupTextConfiguration.Default, popupParams);
        }
        
        GridBuildingSystem.Demolish(transform.position);
      }
    }

    public void Heal(float amount) {
      Health.Heal(amount);
      OnDataChange.Invoke(new Dictionary<DataType, object>() {
        { DataType.MaxHealth, Health.MaxHealth },
        { DataType.CurrentEnergy, Health.CurrentHealth }
      });
    }

    public override Dictionary<DataType, object> GetData() {
      return new Dictionary<DataType, object>() {
        { DataType.Name, Name },
        { DataType.MaxHealth, Health.MaxHealth },
        { DataType.CurrentHealth, Health.CurrentHealth },
      };
    }

    public override void Demolish() {
      sfxEventChannel.RaisePlayEvent(destroySFX, audioConfiguration, Position);
      destroyParmaeters.Position = Position;
      destroyEventChannel.RaiseSpawnEvent(destroyConfiguration, destroyParmaeters);
      base.Demolish();
    }
  }
}
