using Pathfinding;
using UnityEngine;

namespace bts {
  public class Obstacle : MonoBehaviour, Selectable, Damageable {
    public Transform Transform => transform;
    public Selectable.Affiliation ObjectAffiliation => Selectable.Affiliation.Neutral;
    public Selectable.Type ObjectType => Selectable.Type.Obstacle;

    Health health;
    WorldHealthBar bar;
    GameObject selected;
    Collider obstacle;

    void Awake() {
      obstacle = GetComponent<Collider>();
      health = new Health(10);
      bar = GetComponentInChildren<WorldHealthBar>();
      selected = transform.Find("Selected").gameObject;
    }

    void Start() {
      obstacle.enabled = true;
      AstarPath.active.UpdateGraphs(obstacle.bounds);
      bar.SetUp(health);
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
        Destroy(gameObject, .5f);
        obstacle.enabled = false;
        AstarPath.active.UpdateGraphs(obstacle.bounds);
      }
    }
  }
}
