using System;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.Pool;

namespace bts {
  public class Enemy : MonoBehaviour, Selectable, Damageable {
    [SerializeField][Range(0, 1)] float addResourceChance = 0.3f;
    [SerializeField] GemstoneStorage storage;
    [field: SerializeField] public SFXEventChannel SFXEventChannel { get; private set; }
    [field: SerializeField] public BloodEventChannel BloodEventChannel { get; private set; }
    [field: SerializeField] public BloodConfiguration BloodConfig { get; private set; }
    [field: SerializeField] public SelectablesEventChannel SelectablesEventChannel { get; private set; }
    [field: SerializeField] public VoidEventChannel DayStarted { get; private set; }

    readonly BloodParameters bloodParameters = new BloodParameters();
    public IObjectPool<Enemy> Pool { get; set; }
    bool wasPooled;
    
    [field: SerializeField] public EnemyData EnemyData { get; private set; }
    [field: SerializeField] public GameObject Selected { get; private set; }
    
    public bool IsDead { get; private set; }
    public bool IsIntact => Health.HasFullHealth;
    [SerializeField] WorldHealthBar bar;
    public Health Health { get; private set; }
    
    public string Name => "Enemy";
    public Affiliation ObjectAffiliation => Affiliation.Enemy;
    public Type ObjectType => Type.Unit;
    public Transform Center => transform;
    public Vector3 Position => Center.position;
    
    public EnemyStateMachine StateMachine { get; private set; }
    public Damageable Target { get; set; }
    
    public Bounds Bounds => enemyCollider.bounds;
    Collider enemyCollider;
    
    public AIDestinationSetter AIDestinationSetter { get; private set; }
    public Pathfinder Pathfinder { get; private set; }

    void Awake() {
      enemyCollider = GetComponent<Collider>();

      Pathfinder = GetComponent<Pathfinder>();
      AIDestinationSetter = GetComponent<AIDestinationSetter>();
      
      StateMachine = new EnemyStateMachine(this);
      Health = new Health(EnemyData.MaxHealth);
      bar.SetUp(Health);
      DayStarted.OnEventInvoked += Release;
    }

    void OnEnable() {
      IsDead = false;
      wasPooled = false;
      Health.Reset();
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
        BloodEventChannel.RaiseVFXEvent(BloodConfig, bloodParameters);
        SFXEventChannel.OnSFXPlayRequest(EnemyData.EnemyDeathSFX, EnemyData.AudioConfig, Position);
        Pool.Release(this);
        wasPooled = true;
      }
    }

    void Update() {
      StateMachine.Update();
    }

    public void Select() {
      Selected.SetActive(true);
      SFXEventChannel.OnSFXPlayRequest(EnemyData.EnemySelectSFX, EnemyData.AudioConfig, Position);
    }

    public void Deselect() {
      Selected.SetActive(false);
    }

    public bool IsSameAs(Selectable other) {
      return false;
    }

    public Dictionary<string, object> GetData() {
      return new Dictionary<string, object>() {
        { "Health", bar.Health }
      };
    }

    public void TakeDamage(float amount) {
      Health.Damage(amount);
      if (!IsDead && Health.HasNoHealth) {
        IsDead = true;
        if (Selected.activeSelf) {
          SelectablesEventChannel.Invoke(this);
        }

        if (UnityEngine.Random.value < addResourceChance) {
          storage.StoreRandom(UnityEngine.Random.Range(1, 3));
        }
        
        Release();
      }
    }

    public void Heal(float amount) {
      Health.Heal(amount);
    }
  }
}
