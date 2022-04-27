using UnityEngine;
using UnityEngine.AI;

namespace bts {
  public class Unit : MonoBehaviour, Selectable, Moveable {
    NavMeshAgent agent;
    Vector3 destination;
    Damageable target;
    
    void Awake() {
      agent = GetComponent<NavMeshAgent>();
      destination = transform.position;
    }

    public void Select() {
      Debug.Log("Unit selected!");
    }

    public void MoveTo(Vector3 positon) {
      destination = positon;
    }

    void Update() {
      agent.destination = destination;

      if (target != null) {
        if (Vector3.Distance(transform.position, destination) < 2.5f) {
          target.TakeDamage(5);
          target = null;
          destination = transform.position;
        }
      }
    }

    public void Execute(Ray ray) {
      if (Physics.Raycast(ray, out RaycastHit hitInfo)) {
        if (hitInfo.transform.TryGetComponent(out Damageable damageable)) {
          target = damageable;
          MoveTo(hitInfo.transform.position);
        }
        else {
          target = null;
          MoveTo(hitInfo.point);
        }
      }
    }
  }
}
