using System;
using System.Collections.Generic;
using fro.HealthSystem;
using UnityEngine;
using UnityEngine.Pool;

namespace bts {
  public class EnemySpawner : MonoBehaviour, Selectable, Damageable {
    [SerializeField] VoidEventChannel spawnerDeathEventChannel;
    public event Action<Dictionary<DataType, object>> OnDataChange = delegate { };
    [field: SerializeField] public VoidEventChannel DayStarted { get; private set; }
    [field: SerializeField] public VoidEventChannel NightStarted { get; private set; }
    [field: SerializeField] public float SpawnInterval { get; private set; }
    [field: SerializeField] List<Transform> spawnPositions;
    int lastSpawnPosition;
    [SerializeField] Enemy enemyPrefab;
    public ObjectPool<Enemy> EnemyPool { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }

    public string Name => "Spawner";
    public Affiliation ObjectAffiliation => Affiliation.Enemy;
    public Type ObjectType => Type.Building;
    [field: SerializeField] public Transform Center { get; private set; }
    [field: SerializeField] public GameObject Selected { get; private set; }
    [field: SerializeField] public SelectablesEventChannel SelectablesEventChannel { get; private set; }
    public Vector3 Position => Center.position;
    public bool IsDead => healthComponent.Health.IsDead;
    [SerializeField] HealthComponent healthComponent;
    [SerializeField] float maxHealth = 1e30f;
    public Bounds Bounds => spawnerCollider.bounds;
    Collider spawnerCollider;

    EnemySpawnerStateMachine stateMachine;

    void Awake() {
      stateMachine = new EnemySpawnerStateMachine(this);
      healthComponent.Init(maxHealth);
      EnemyPool = new ObjectPool<Enemy>(Create, Get, Release);
      spawnerCollider = GetComponent<Collider>();
      healthComponent.Health.OnCurrentHealthChange += OnCurrentHealthChange;
      healthComponent.Health.OnDie += OnDie;
    }

    void Update() {
      stateMachine.Update();
    }

    void OnDestroy() {
      healthComponent.Health.OnCurrentHealthChange -= OnCurrentHealthChange;
      healthComponent.Health.OnDie -= OnDie;
    }

    void OnCurrentHealthChange(object sender, EventArgs e) {
      OnDataChange.Invoke(GetHealthData());
    }

    void OnDie(object sender, EventArgs e) {
      if (Selected.activeSelf) {
        SelectablesEventChannel.Invoke(this);
      }

      spawnerDeathEventChannel.Invoke();
      Destroy(gameObject);
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
      Dictionary<DataType, object> data = GetHealthData();
      data.Add(DataType.Name, Name);
      return data;
    }

    Dictionary<DataType, object> GetHealthData() {
      return new Dictionary<DataType, object>() {
        { DataType.MaxHealth, healthComponent.GetMaxHealth() },
        { DataType.CurrentHealth, healthComponent.GetCurrentHealth() }
      };
    }

    public void TakeDamage(float amount) {
      healthComponent.Damage(amount);
    }
  }
}
