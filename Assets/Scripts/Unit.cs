using Pathfinding;
using UnityEngine;

namespace bts {
  public class Unit : MonoBehaviour, Selectable {
    public Transform Transform => transform;
    public Affiliation ObjectAffiliation => Affiliation.Player;
    public Type ObjectType => Type.Unit;
    public GameObject Selected { get; private set; }

    PlayerInputs playerInputs;
    Camera cam;
    AIPath aiPath;
    Vector3 destination;
    Damageable target;
    bool isSelected;

    float lastAttackTime;
    const float timeBetweenAttacks = 1f;

    void Awake() {
      playerInputs = FindObjectOfType<PlayerInputs>();
      cam = Camera.main;
      aiPath = GetComponent<AIPath>();
      Selected = transform.Find("Selected").gameObject;
    }

    public void Select() {
      Selected.SetActive(true);
      isSelected = true;
    }

    public void Deselect() {
      Selected.SetActive(false);
      isSelected = false;
    }

    public void MoveTo(Vector3 positon) {
      aiPath.destination = positon;
    }

    void Update() {
      if (isSelected && playerInputs.IsRightBtnDawn) {
        Ray ray = cam.ScreenPointToRay(playerInputs.MouseScreenPosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo)) {
          if (hitInfo.transform.TryGetComponent(out Damageable damageable) && damageable.ObjectAffiliation != Affiliation.Player) {
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

      if (target != null && (target as Object) != null) {
        if (Vector3.Distance(transform.position, destination) < 2.5f) {
          if (lastAttackTime + timeBetweenAttacks < Time.time) {
            target?.TakeDamage(5);
            lastAttackTime = Time.time;
          }
        }
      }
    }
  }
}
