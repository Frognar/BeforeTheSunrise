using UnityEngine;
using UnityEngine.AI;

namespace bts {
  public class Unit : MonoBehaviour, Selectable, Moveable {
    NavMeshAgent agent;
    Vector3 destination;

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
    }
  }
}
