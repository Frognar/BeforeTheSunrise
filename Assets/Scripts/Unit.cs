using Pathfinding;
using UnityEngine;

namespace bts {
  public class Unit : MonoBehaviour, Selectable {
    public Transform Transform => transform;
    public Selectable.Affiliation ObjectAffiliation => Selectable.Affiliation.Player;
    public Selectable.Type ObjectType => Selectable.Type.Unit;

    GameObject selected;
    AIPath aiPath;
    Vector3 destination;
    Damageable target;

    float lastAttackTime;
    const float timeBetweenAttacks = 1f;
    
    void Awake() {
      aiPath = GetComponent<AIPath>();
      selected = transform.Find("Selected").gameObject;
    }

    public void Select() {
      selected.SetActive(true);
    }

    public void Deselect() {
      selected.SetActive(false);
    }

    public void MoveTo(Vector3 positon) {
      aiPath.destination = positon;
    }

    void Update() {
      if (target != null && (target as Object) != null) {
        if (Vector3.Distance(transform.position, destination) < 2.5f) {
          if (lastAttackTime + timeBetweenAttacks < Time.time) {
            target?.TakeDamage(5);
            lastAttackTime = Time.time;
          }
        }
      }
    }

    public void Execute(Ray ray) {
      if (Physics.Raycast(ray, out RaycastHit hitInfo)) {
        if (hitInfo.transform.TryGetComponent(out Damageable damageable)) {
          target = damageable;
          destination = hitInfo.transform.position;
          MoveTo(destination);
        }
        else {
          target = null;
          MoveTo(hitInfo.point);
        }
      }
    }
  }
}
