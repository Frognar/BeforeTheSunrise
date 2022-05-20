using UnityEngine;

namespace bts {
  public class EnemyTestObject : MonoBehaviour, Selectable, Damageable {
    [SerializeField] Transform center;
    public string Name => "Test enemy";
    public Transform Transform => center;
    public Affiliation ObjectAffiliation => Affiliation.Enemy;
    public Type ObjectType => Type.Unit;
    public GameObject Selected { get; private set; }
    public Vector3 Position => center.position;
    public bool IsDead => health.CurrentHealth == 0;

    [SerializeField] int healthAmount = 10;
    Health health;
    WorldHealthBar bar;

    protected virtual void Start() {
      health = new Health(healthAmount);
      Selected = transform.Find("Selected").gameObject;
      bar = GetComponentInChildren<WorldHealthBar>();
      bar.SetUp(health);
    }

    public void TakeDamage(int amount) {
      health.Damage(amount);
      if (health.CurrentHealth == 0) {
        Destroy(gameObject, 0.1f);
      }
    }

    public void Select() {
      Selected.SetActive(true);
    }

    public void Deselect() {
      Selected.SetActive(false);
    }
  }
}
