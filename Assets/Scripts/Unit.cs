using UnityEngine;
using UnityEngine.AI;

namespace bts {
  public class Unit : MonoBehaviour, Selectable, Moveable {
    public Transform Transform => transform;

    GameObject selected;
    NavMeshAgent agent;
    Vector3 destination;
    Damageable target;

    float lastAttackTime;
    float timeBetweenAttacks = 1f;
    
    void Awake() {
      agent = GetComponent<NavMeshAgent>();
      selected = transform.Find("Selected").gameObject;
    }

    public void Select() {
      selected.SetActive(true);
    }

    public void Deselect() {
      selected.SetActive(false);
    }

    public void MoveTo(Vector3 positon) {
      agent.SetDestination(positon);
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
