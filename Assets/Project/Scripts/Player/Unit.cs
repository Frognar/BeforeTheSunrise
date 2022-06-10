using System;
using System.Collections.Generic;
using UnityEngine;

namespace bts {
  public class Unit : MonoBehaviour, Selectable, Damageable, CommandReceiver {
    [SerializeField] VoidEventChannel deathEventChannel;
    public event Action<Dictionary<DataType, object>> OnDataChange = delegate { };
    [field: Header("SFX")]
    [field: SerializeField] public SFXEventChannel SFXEventChannel { get; private set; }
    [field: SerializeField] public AudioConfiguration AudioConfig { get; private set; }
    [field: SerializeField] public AudioClipsGroup AttackSFX { get; private set; }
    [field: SerializeField] public AudioClipsGroup TakeDamageSFX { get; private set; }
    [field: SerializeField] public AudioClipsGroup DieSFX { get; private set; }
    [field: Header("VFX")]
    [field: SerializeField] public ElectricArcEventChannel VFXEventChannel { get; private set; }
    [field: SerializeField] public Transform ArcBegin { get; private set; }
    [field: SerializeField] public ElectricArcVFXConfiguration ElectricArcConfig { get; private set; }
    
    public string Name => "Unit";
    public Transform Center => transform;
    public Affiliation ObjectAffiliation => Affiliation.Player;
    public Type ObjectType => Type.Unit;
    [field: Header("")]
    [field: SerializeField] public PopupTextEventChannel PopupTextEventChannel { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public GameObject Selected { get; private set; }
    [field: SerializeField] public SelectablesEventChannel SelectablesEventChannel { get; private set; }
    [SerializeField] List<BuildUICommandData> buildCommandsData;
    [SerializeField] CancelBuildUICommandData cancelBuildCommandData;
    public IEnumerable<UICommand> UICommands { get; private set; }
    public bool IsSelected { get; private set; }
    public GridBuildingSystem GridBuildingSystem { get; private set; }
    public Pathfinder Pathfinder { get; private set; }

    public bool IsIdle { get; set; }
    public bool IsGathering { get; set; }

    
    [field: SerializeField] public GemstoneStorage GemstoneStorage { get; private set; }
    [SerializeField] UnitStats stats;
    public float GatherRange => 3f;
    public float TimeBetweenGathers => stats.timeBetweenGathers;
    public GemstoneDictionary GatherBonuses => stats.gatherBonuses;
    public bool IsOrderedToGather { get; set; }
    public Gemstone TargerGemstone { get; set; }

    public float BuildRange => 3.5f;
    public bool IsOrderedToBuild { get; set; }
    public PlacedObjectType BuildingToPlace { get; set; }

    public float StopDistance => 1f;
    public bool IsOrderedToMove { get; set; }
    public Vector3 Destination { get; set; }

    public float DamageAmount => stats.damageAmount;
    public float TimeBetweenAttacks => stats.timeBetweenAttacks;
    public float AttackRange => 4f;
    public bool IsOrderedToAttack { get; set; }
    public Damageable Target { get; set; }
    public Vector3 Position => transform.position;
    Collider unitCollider;
    public Bounds Bounds => unitCollider.bounds;
    public bool IsDead => Health.IsDead;
    public bool IsIntact => Health.IsInFullHealth;
    public Health Health => HealthComponent.Health;
    [field: SerializeField] public HealthComponent HealthComponent { get; private set; }

    StateMachine<Unit> stateMachine;

    void Awake() {
      HealthComponent.Init(stats.MaxHealth);
      UICommands = CreateActions();
      GridBuildingSystem = FindObjectOfType<GridBuildingSystem>();
      Pathfinder = GetComponent<Pathfinder>();
      unitCollider = GetComponent<Collider>();
      stateMachine = new UnitStateMachine(this);
    }

    List<UICommand> CreateActions() {
      List<UICommand> commands = new List<UICommand>();
      UnitCommander commander = GetComponent<UnitCommander>();
      foreach (BuildUICommandData data in buildCommandsData) {
        commands.Add(new BuildUICommand(data, commander));
      }

      commands.Add(new CancelBuildUICommand(cancelBuildCommandData, commander));

      return commands;
    }

    void Start() {
      GemstoneStorage.Reset();
      Pathfinder.SetSpeed(stats.MovementSpeed);
      stateMachine.Start();
    }

    void Update() {
      stateMachine.Update();
    }

    public void Select() {
      Selected.SetActive(true);
      IsSelected = true;
    }

    public bool IsSameAs(Selectable other) {
      return other is Unit;
    }

    public void Deselect() {
      Selected.SetActive(false);
      IsSelected = false;
    }

    public Dictionary<DataType, object> GetData() {
      return new Dictionary<DataType, object>() {
        { DataType.Name, Name },
        { DataType.MaxHealth, Health.MaxHealth },
        { DataType.CurrentHealth, Health.CurrentHealth },
        { DataType.MovementSpeed, stats.MovementSpeed },
        { DataType.DamagePerSecond, stats.damageAmount / stats.timeBetweenAttacks },
      };
    }

    void OnDestroy() {
      if (IsSelected) {
        SelectablesEventChannel.Invoke(this);
      }

      deathEventChannel.Invoke();
    }

    public void TakeDamage(float amount) {
      Health.Damage(amount);
      OnDataChange.Invoke(new Dictionary<DataType, object>() {
        { DataType.MaxHealth, Health.MaxHealth },
        { DataType.CurrentHealth, Health.CurrentHealth }
      });
      
      if (IsDead) {
        SFXEventChannel.RaisePlayEvent(DieSFX, AudioConfig, Position);
        Destroy(gameObject);
      }
      else {
        SFXEventChannel.RaisePlayEvent(TakeDamageSFX, AudioConfig, Position);
      }
    }

    public void Heal(float amount) {
      Health.Heal(amount);
      OnDataChange.Invoke(new Dictionary<DataType, object>() {
        { DataType.MaxHealth, Health.MaxHealth },
        { DataType.CurrentHealth, Health.CurrentHealth }
      });
    }

    public bool IsFree() {
      return IsIdle || IsGathering;
    }

    void OnEnable() {
      stats.OnSpeedUpgrade += UpgradeSpeed;
      stats.OnHealthUpgrade += UpgradeHealth;
    }

    void UpgradeHealth() {
      Health.ChangeMaxHealth(stats.MaxHealth);
    }

    void UpgradeSpeed() {
      Pathfinder.SetSpeed(stats.MovementSpeed);
    }

    void OnDisable() {
      stats.OnSpeedUpgrade -= UpgradeSpeed;
      stats.OnHealthUpgrade -= UpgradeHealth;
    }
  }
}