using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace bts {
  public class EnemySpawner : MonoBehaviour, Selectable, Damageable {
    [field: SerializeField] public VoidEventChannel DayStarted { get; private set; }
    [field: SerializeField] public VoidEventChannel NightStarted { get; private set; }
    [field: SerializeField] public float SpawnInterval { get; private set; }
    [field: SerializeField] List<Transform> spawnPositions;
    int lastSpawnPosition;
    [SerializeField] Enemy enemyPrefab;
    public ObjectPool<Enemy> EnemyPool { get; private set; }

    public string Name => "Big Bad Boi";
    public Affiliation ObjectAffiliation => Affiliation.Enemy;
    public Type ObjectType => Type.Building;
    [field: SerializeField] public Transform Center { get; private set; }
    [field: SerializeField] public GameObject Selected { get; private set; }
    [field: SerializeField] public SelectablesEventChannel SelectablesEventChannel { get; private set; }
    public Vector3 Position => Center.position;
    public bool IsDead => Health.HasNoHealth;
    public bool IsIntact => Health.HasFullHealth;

    [SerializeField] WorldHealthBar bar;
    [SerializeField] GridBuildingSystem gridBuildingSystem;
    public Health Health { get; private set; }
    public Bounds Bounds => spawnerCollider.bounds;
    Collider spawnerCollider;

    EnemySpawnerStateMachine stateMachine;

    void Awake() {
      stateMachine = new EnemySpawnerStateMachine(this);
      Health = new Health(int.MaxValue);
      bar.SetUp(Health);
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
      Vector3Int cords = gridBuildingSystem.Grid.GetCords(transform.position);
      List<Vector3Int> gridPositions = new List<Vector3Int>();
      for (int x = 0; x < 16; x++) {
        for (int z = 0; z < 16; z++) {
          gridPositions.Add(cords + new Vector3Int(x, 0, z));
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

    public Dictionary<string, object> GetData() {
      return new Dictionary<string, object>() {
        { "Health", bar.Health }
      };
    }

    public void TakeDamage(float amount) {
      Health.Damage(amount);
      if (IsDead && Selected.activeSelf) {
        SelectablesEventChannel.Invoke(this);
      }
    }

    public void Heal(float amount) {
      Health.Heal(amount);
    }
  }
}
