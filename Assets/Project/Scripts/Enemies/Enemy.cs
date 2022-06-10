using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace bts {
  public class Enemy : MonoBehaviour, Selectable, Damageable {
    [SerializeField] PopupTextEventChannel popupTextEventChannel;
    public event Action<Dictionary<DataType, object>> OnDataChange = delegate { };
    [SerializeField][Range(0, 1)] float addResourceChance = 0.35f;
    [SerializeField] GemstoneStorage storage;
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
    
    public Pathfinder Pathfinder { get; private set; }

    void Awake() {
      enemyCollider = GetComponent<Collider>();
      Pathfinder = GetComponent<Pathfinder>();
      StateMachine = new EnemyStateMachine(this);
      Health = new Health(EnemyData.MaxHealth);
      bar.SetUp(Health);
      DayStarted.OnEventInvoked += Release;
    }

    void Start() {
      Pathfinder.SetSpeed(EnemyData.MovementSpeed);
    }

    void OnEnable() {
      IsDead = false;
      wasPooled = false;
      Health.Upgrade(EnemyData.MaxHealth);
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
      return new Dictionary<DataType, object>() {
        { DataType.Name, Name },
        { DataType.MaxHealth, Health.MaxHealth },
        { DataType.CurrentHealth, bar.Health.CurrentHealth },
        { DataType.DamagePerSecond, EnemyData.DamagePerSecond },
        { DataType.MovementSpeed, EnemyData.MovementSpeed },
      };
    }

    public void TakeDamage(float amount) {
      Health.Damage(amount);
      OnDataChange.Invoke(new Dictionary<DataType, object>() {
        { DataType.MaxHealth, Health.MaxHealth },
        { DataType.CurrentHealth, bar.Health.CurrentHealth }
      });
      
      if (!IsDead && Health.HasNoHealth) {
        IsDead = true;
        if (Selected.activeSelf) {
          SelectablesEventChannel.Invoke(this);
        }

        if (UnityEngine.Random.value < addResourceChance) {
          GemstoneType type = storage.GetRandomType();
          int count = UnityEngine.Random.Range(1, 3);
          storage.Store(type, count);
          PopupTextParameters popupParams = new PopupTextParameters() {
            Position = Center.position,
            GemstoneType = type,
            Text = $"+{count}"
          };

          popupTextEventChannel.RaiseSpawnEvent(PopupTextConfiguration.Default, popupParams);
        }
        
        Release();
      }
    }

    public void Heal(float amount) {
      Health.Heal(amount);
      OnDataChange.Invoke(new Dictionary<DataType, object>() {
        { DataType.MaxHealth, Health.MaxHealth },
        { DataType.CurrentEnergy, bar.Health.CurrentHealth }
      });
    }
  }
}
