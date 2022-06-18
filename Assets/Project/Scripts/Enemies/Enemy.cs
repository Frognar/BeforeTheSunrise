using System;
using System.Collections.Generic;
using bts.Gemstones;
using fro.HealthSystem;
using UnityEngine;
using UnityEngine.Pool;

namespace bts {
  public class Enemy : MonoBehaviour, Selectable, Damageable {
    [SerializeField] RandomGemstoneGiver randomGemstoneGiver;
    public event Action<Dictionary<DataType, object>> OnDataChange = delegate { };
    [field: SerializeField] public SFXEventChannel SFXEventChannel { get; private set; }
    [field: SerializeField] public BloodEventChannel BloodEventChannel { get; private set; }
    [field: SerializeField] public BloodConfiguration BloodConfig { get; private set; }
    [field: SerializeField] public SelectablesEventChannel SelectablesEventChannel { get; private set; }
    [field: SerializeField] public VoidEventChannel DayStarted { get; private set; }

    readonly BloodParameters bloodParameters = new BloodParameters();
    public IObjectPool<Enemy> Pool { get; set; }
    bool wasPooled;
    public Sprite Icon => EnemyData.Icon;
    [field: SerializeField] public EnemyData EnemyData { get; private set; }
    [field: SerializeField] public GameObject Selected { get; private set; }
    
    public bool IsDead { get; private set; }
    public bool IsIntact => HealthComponent.Health.IsInFullHealth;
    [field: SerializeField] public HealthComponent HealthComponent { get; private set; }

    public string Name => "Enemy";
    public Affiliation ObjectAffiliation => Affiliation.Enemy;
    public Type ObjectType => Type.Unit;
    public Transform Center => transform;
    public Vector3 Position => Center.position;
    
    public EnemyStateMachine StateMachine { get; private set; }
    public Damageable Target { get; set; }
    
    public Bounds Bounds => enemyCollider.bounds;
    Collider enemyCollider;
    
    public Pathfinder Pathfinder { get; private set; }

    void Awake() {
      enemyCollider = GetComponent<Collider>();
      Pathfinder = GetComponent<Pathfinder>();
      StateMachine = new EnemyStateMachine(this);
      HealthComponent.Init(EnemyData.MaxHealth);
      DayStarted.OnEventInvoked += Release;
    }

    void Start() {
      Pathfinder.SetSpeed(EnemyData.MovementSpeed);
    }

    void OnEnable() {
      IsDead = false;
      wasPooled = false;
      HealthComponent.ChangeMaxHealth(EnemyData.MaxHealth);
      HealthComponent.ResetHealth();
    }

    void OnDestroy() {
      DayStarted.OnEventInvoked -= Release;
    }

    void Release(object s, EventArgs e) {
      Release();
    }

    void Release() {
      if (!wasPooled) {
        bloodParameters.Position = Position;
        BloodEventChannel.RaiseSpawnEvent(BloodConfig, bloodParameters);
        SFXEventChannel.RaisePlayEvent(EnemyData.EnemyDeathSFX, EnemyData.AudioConfig, Position);
        Pool.Release(this);
        wasPooled = true;
      }
    }

    void Update() {
      StateMachine.Update();
    }

    public void Select() {
      Selected.SetActive(true);
      SFXEventChannel.RaisePlayEvent(EnemyData.EnemySelectSFX, EnemyData.AudioConfig, Position);
    }

    public void Deselect() {
      Selected.SetActive(false);
    }

    public bool IsSameAs(Selectable other) {
      return false;
    }

    public Dictionary<DataType, object> GetData() {
      Dictionary<DataType, object> data = GetHealthData();
      data.Add(DataType.Name, Name);
      data.Add(DataType.DamagePerSecond, EnemyData.DamagePerSecond);
      data.Add(DataType.MovementSpeed, EnemyData.MovementSpeed);
      return data;
    }

    public void TakeDamage(float amount) {
      HealthComponent.Damage(amount);
      OnDataChange.Invoke(GetHealthData());
      if (!IsDead && HealthComponent.Health.IsDead) {
        IsDead = true;
        if (Selected.activeSelf) {
          SelectablesEventChannel.Invoke(this);
        }

        randomGemstoneGiver.Give();
        Release();
      }
    }

    public void Heal(float amount) {
      HealthComponent.Heal(amount);
      OnDataChange.Invoke(GetHealthData());
    }

    Dictionary<DataType, object> GetHealthData() {
      return new Dictionary<DataType, object>() {
        { DataType.MaxHealth, HealthComponent.GetMaxHealth() },
        { DataType.CurrentHealth, HealthComponent.GetCurrentHealth() }
      };
    }
  }
}
