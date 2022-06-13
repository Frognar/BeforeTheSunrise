using System;
using System.Collections.Generic;
using fro.BuildingSystem;
using fro.HealthSystem;
using UnityEngine;
using UnityEngine.Pool;

namespace bts {
  public class EnemySpawner : MonoBehaviour, Selectable, Damageable {
    [field: SerializeField] public EnemyData EnemyData { get; private set; }
    [field: SerializeField] public IntAsset DayCounter { get; private set; }
    public event Action<Dictionary<DataType, object>> OnDataChange = delegate { };
    [field: SerializeField] public VoidEventChannel DayStarted { get; private set; }
    [field: SerializeField] public VoidEventChannel NightStarted { get; private set; }
    [field: SerializeField] public float SpawnInterval { get; private set; }
    [field: SerializeField] List<Transform> spawnPositions;
    int lastSpawnPosition;
    [SerializeField] Enemy enemyPrefab;
    public ObjectPool<Enemy> EnemyPool { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }

    public string Name => "Big Bad Boi";
    public Affiliation ObjectAffiliation => Affiliation.Enemy;
    public Type ObjectType => Type.Building;
    [field: SerializeField] public Transform Center { get; private set; }
    [field: SerializeField] public GameObject Selected { get; private set; }
    [field: SerializeField] public SelectablesEventChannel SelectablesEventChannel { get; private set; }
    public Vector3 Position => Center.position;
    public bool IsDead => Health.IsDead;
    public bool IsIntact => Health.IsInFullHealth;
    Health Health => healthComponent.Health;
    [SerializeField] HealthComponent healthComponent;
    [SerializeField] GridBuildingSystem gridBuildingSystem;
    public Bounds Bounds => spawnerCollider.bounds;
    Collider spawnerCollider;

    EnemySpawnerStateMachine stateMachine;

    void Awake() {
      stateMachine = new EnemySpawnerStateMachine(this);
      healthComponent.Init(float.MaxValue);
      EnemyPool = new ObjectPool<Enemy>(Create, Get, Release);
      spawnerCollider = GetComponent<Collider>();
    }

    Enemy Create() {
      Enemy enemy = Instantiate(enemyPrefab, transform);
      enemy.Pool = EnemyPool;
      return enemy;
    }

    void Get(Enemy e) {
      e.gameObject.SetActive(true);
      e.StateMachine.Start();
      e.transform.position = GetSpawnPosition();
    }
    
    Vector3 GetSpawnPosition() {
      Vector3 spawn = spawnPositions[lastSpawnPosition].position;
      lastSpawnPosition = (lastSpawnPosition + 1) % spawnPositions.Count;
      return spawn;
    }

    void Release(Enemy e) {
      e.Target = null;
      e.gameObject.SetActive(false);
    }
    
    void Start() {
      GridCords cords = gridBuildingSystem.Grid.GetCords(transform.position);
      List<Vector3Int> gridPositions = new List<Vector3Int>();
      for (int x = 0; x < 16; x++) {
        for (int z = 0; z < 16; z++) {
          gridPositions.Add(new Vector3Int(cords.X + x, 0, cords.Z + z));
        }
      }

      gridBuildingSystem.Block(gridPositions);
    }

    void Update() {
      stateMachine.Update();
    }

    public void Select() {
      Selected.SetActive(true);
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
        { DataType.CurrentHealth, Health.CurrentHealth },
      };
    }

    public void TakeDamage(float amount) {
      Health.Damage(amount);
      OnDataChange.Invoke(new Dictionary<DataType, object>() {
        { DataType.MaxHealth, Health.MaxHealth },
        { DataType.CurrentHealth, Health.CurrentHealth }
      });
      
      if (IsDead && Selected.activeSelf) {
        SelectablesEventChannel.Invoke(this);
      }
    }

    public void Heal(float amount) {
      Health.Heal(amount);
      OnDataChange.Invoke(new Dictionary<DataType, object>() {
        { DataType.MaxHealth, Health.MaxHealth },
        { DataType.CurrentEnergy, Health.CurrentHealth }
      });
    }
  }
}
