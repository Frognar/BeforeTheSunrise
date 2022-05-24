using System;
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
    public bool IsDead => Health.CurrentHealth == 0;
    [SerializeField] WorldHealthBar bar;
    public Health Health { get; private set; }
    EnemyStateMachine stateMachine;

    void Awake() {
      stateMachine = new EnemyStateMachine(this);
      Health = new Health(EnemyData.MaxHealth);
      bar.SetUp(Health);
    }

    void OnEnable() {
      DayStarted.OnEventInvoked += Release;
    }

    void OnDisable() {
      DayStarted.OnEventInvoked -= Release;
    }

    void Release(object s, EventArgs e) {
      Pool.Release(this);
    }

    void Start() {
      stateMachine.Start();
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

    public void TakeDamage(int amount) {
      Health.Damage(amount);
      if (IsDead && Selected.activeSelf) {
        SelectablesEventChannel.Invoke(this);
        Pool.Release(this);
      }
    }
  }
}
