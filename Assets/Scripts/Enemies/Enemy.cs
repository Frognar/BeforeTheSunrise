using System;
using Pathfinding;
using UnityEngine;
using UnityEngine.Pool;

namespace bts {
  public class Enemy : MonoBehaviour, Selectable, Damageable {
    [field: SerializeField] public VoidEventChannel DayStarted { get; private set; }
    public IObjectPool<Enemy> Pool { get; set; }
    [field: SerializeField] public EnemyData EnemyData { get; private set; }
    public string Name => "Enemy";
    public Affiliation ObjectAffiliation => Affiliation.Enemy;
    public Type ObjectType => Type.Unit;
    public Transform Transform => transform;
    [field: SerializeField] public GameObject Selected { get; private set; }
    [field: SerializeField] public SelectablesEventChannel SelectablesEventChannel { get; private set; }
    public Vector3 Position => Transform.position;
    public Seeker Seeker { get; private set; }
    public AIPath AiPath { get; private set; }
    public AIDestinationSetter AIDestinationSetter { get; private set; }
    public bool IsDead { get; private set; }
    [SerializeField] WorldHealthBar bar;
    public Health Health { get; private set; }
    public EnemyStateMachine StateMachine { get; private set; }
    public Damageable Target { get; set; }
    public Bounds Bounds => enemyCollider.bounds;
    Collider enemyCollider;

    void Awake() {
      enemyCollider = GetComponent<Collider>();
      Seeker = GetComponent<Seeker>();
      AiPath = GetComponent<AIPath>();
      AIDestinationSetter = GetComponent<AIDestinationSetter>();
      StateMachine = new EnemyStateMachine(this);
      Health = new Health(EnemyData.MaxHealth);
      bar.SetUp(Health);
      DayStarted.OnEventInvoked += Release;
    }

    void OnEnable() {
      IsDead = false;
      Health.Reset();
    }

    void OnDestroy() {
      DayStarted.OnEventInvoked -= Release;
    }

    void Release(object s, EventArgs e) {
      if (!IsDead) {
        Pool.Release(this);
      }
    }

    void Update() {
      StateMachine.Update();
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

    public void TakeDamage(int amount) {
      Health.Damage(amount);
      if (!IsDead && Health.CurrentHealth == 0) {
        Pool.Release(this);
        IsDead = true;
        if (Selected.activeSelf) {
          SelectablesEventChannel.Invoke(this);
        }
      }
    }
  }
}
