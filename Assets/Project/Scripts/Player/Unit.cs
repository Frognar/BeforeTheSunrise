using System;
using System.Collections.Generic;
using bts.Gemstones;
using fro.BuildingSystem;
using fro.HealthSystem;
using fro.States;
using UnityEngine;

namespace bts {
  public class Unit : MonoBehaviour, Selectable, Damageable, CommandReceiver {
    [SerializeField] VoidEventChannel deathEventChannel;
    [field: SerializeField] public PopupTextEventChannel PopupTextEventChannel { get; private set; }
    [field: SerializeField] public SelectablesEventChannel SelectablesEventChannel { get; private set; }
    public event Action<Dictionary<DataType, object>> OnDataChange = delegate { };
    
    [field: Header("SFX")]
    [field: SerializeField] public AudioRequester AudioRequester { get; private set; }
    [field: SerializeField] public AudioClipsGroup AttackSFX { get; private set; }
    [field: SerializeField] public AudioClipsGroup TakeDamageSFX { get; private set; }
    [field: SerializeField] public AudioClipsGroup DieSFX { get; private set; }
    
    [field: Header("VFX")]
    [field: SerializeField] public ElectricArcRequester ElectricArcRequester { get; private set; }
    
    public string Name => "Unit";
    public Transform Center => transform;
    public Affiliation ObjectAffiliation => Affiliation.Player;
    public Type ObjectType => Type.Unit;
    
    [field: Space]
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public GameObject Selected { get; private set; }
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
    public PlacedObjectData BuildingToPlace { get; set; }
    public CustomBuildingData CustomBuildingData { get; set; }

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
    public bool IsDead => HealthComponent.Health.IsDead;
    public bool IsIntact => HealthComponent.Health.IsInFullHealth;
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
      Dictionary<DataType, object> data = GetHealthData();
      data.Add(DataType.Name, Name );
      data.Add(DataType.MovementSpeed, stats.MovementSpeed );
      data.Add(DataType.DamagePerSecond, stats.damageAmount / stats.timeBetweenAttacks);
      return data;
    }

    void OnDestroy() {
      if (IsSelected) {
        SelectablesEventChannel.Invoke(this);
      }
    }

    public void TakeDamage(float amount) {
      HealthComponent.Damage(amount);
      OnDataChange.Invoke(GetHealthData());
      
      if (IsDead) {
        AudioRequester.RequestSFX(DieSFX, Position);
        deathEventChannel.Invoke();
        Destroy(gameObject);
      }
      else {
        AudioRequester.RequestSFX(TakeDamageSFX, Position);
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

    public bool IsFree() {
      return IsIdle || IsGathering;
    }

    void OnEnable() {
      stats.OnSpeedUpgrade += UpgradeSpeed;
      stats.OnHealthUpgrade += UpgradeHealth;
    }

    void OnDisable() {
      stats.OnSpeedUpgrade -= UpgradeSpeed;
      stats.OnHealthUpgrade -= UpgradeHealth;
    }

    void UpgradeHealth() {
      HealthComponent.ChangeMaxHealth(stats.MaxHealth);
    }

    void UpgradeSpeed() {
      Pathfinder.SetSpeed(stats.MovementSpeed);
    }
  }
}