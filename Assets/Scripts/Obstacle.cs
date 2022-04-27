using UnityEngine;

namespace bts {
  public class Obstacle : MonoBehaviour, Selectable, Damageable {
    Health health;
    WorldHealthBar bar;

    void Awake() {
      health = new Health(10);
      bar = GetComponentInChildren<WorldHealthBar>();
      bar?.SetUp(health);
    }

    public void Select() {
      Debug.Log("Obstacle selected!");
    }

    public void TakeDamage(int amount) {
      health.Damage(amount);
      if (health.CurrentHealth == 0) {
        Destroy(gameObject);
      }
    }
  }
}
