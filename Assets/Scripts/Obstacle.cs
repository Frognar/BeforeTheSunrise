using Pathfinding;
using UnityEngine;

namespace bts {
  public class Obstacle : MonoBehaviour, Selectable, Damageable {
    public Transform Transform => transform;

    Health health;
    WorldHealthBar bar;
    GameObject selected;
    Collider obstacle;

    void Awake() {
      obstacle = GetComponent<Collider>();
      health = new Health(10);
      bar = GetComponentInChildren<WorldHealthBar>();
      bar.SetUp(health);
      selected = transform.Find("Selected").gameObject;
    }

    public void Select() {
      selected.SetActive(true);
    }

    public void Deselect() {
      selected.SetActive(false);
    }

    public void TakeDamage(int amount) {
      health.Damage(amount);
      if (health.CurrentHealth == 0) {
        obstacle.enabled = false;
        AstarPath.active.UpdateGraphs(obstacle.bounds);
        Destroy(gameObject, .5f);
      }
    }
  }
}
