using System;
using System.Collections.Generic;
using bts.Gemstones;
using fro.HealthSystem;
using UnityEngine;

namespace bts {
  public class Obstacle : MonoBehaviour, Selectable, Damageable {
    public event Action<Dictionary<DataType, object>> OnDataChange = delegate { };

    public string Name => nameof(Obstacle);
    public Affiliation ObjectAffiliation => Affiliation.Neutral;
    public Type ObjectType => Type.Obstacle;
    [field: SerializeField] public Transform Center { get; private set; }
    [field: SerializeField] public GameObject Selected { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public SelectablesEventChannel SelectablesEventChannel { get; private set; }
    public Vector3 Position => Center.position;
    [SerializeField] Collider obstacleCollider;
    public Bounds Bounds => obstacleCollider.bounds;
    
    [Header("Health")]
    [SerializeField] HealthComponent healthComponent;
    public bool IsDead => healthComponent.Health.IsDead;
    public bool IsIntact => healthComponent.Health.IsInFullHealth;
    
    [Header("Gemstones")]
    [SerializeField] RandomGemstoneGiver randomGemstoneGiver;
    
    [Header("Audio")]
    [SerializeField] SFXEventChannel sfxEventChannel;
    [SerializeField] AudioClipsGroup destroySFX;
    [SerializeField] AudioConfiguration audioConfiguration;
    [SerializeField] DestroyEventChannel destroyEventChannel;
    [SerializeField] DestroyConfiguration destroyConfiguration;

    public void Select() {
      Selected.SetActive(true);
    }
    
    public void Deselect() {
      Selected.SetActive(false);
    }
    

    void Start() {
      healthComponent.Init(10f);
    }

    public void TakeDamage(float amount) {
      healthComponent.Damage(amount);
      OnDataChange.Invoke(GetHealthData());

      if (IsDead) {
        randomGemstoneGiver.Give();
        sfxEventChannel.RaisePlayEvent(destroySFX, audioConfiguration, Position);
        destroyEventChannel.RaiseSpawnEvent(destroyConfiguration, new DestroyParameters(Position));
        Destroy(gameObject);
      }
    }

    public void Heal(float amount) {
      healthComponent.Heal(amount);
      OnDataChange.Invoke(GetHealthData());
    }

    public Dictionary<DataType, object> GetData() {
      Dictionary<DataType, object> data = GetHealthData();
      data.Add(DataType.Name, Name);
      return data;
    }

    Dictionary<DataType, object> GetHealthData() {
      return new Dictionary<DataType, object>() {
        { DataType.MaxHealth, healthComponent.GetMaxHealth() },
        { DataType.CurrentHealth, healthComponent.GetCurrentHealth() },
      };
    }

    public bool IsSameAs(Selectable other) {
      return false;
    }
  }
}
