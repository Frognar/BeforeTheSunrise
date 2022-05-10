using Pathfinding;
using UnityEngine;

namespace bts {
  public class Unit : MonoBehaviour, Selectable {
    public string Name => "Unit";
    public Transform Transform => transform;
    public Affiliation ObjectAffiliation => Affiliation.Player;
    public Type ObjectType => Type.Unit;
    public GameObject Selected { get; private set; }
    bool HasTarget => attackTarget != null && (attackTarget as Object) != null;
    bool IsTimeToAttack => lastAttackTime + timeBetweenAttacks < Time.time;
    bool IsOrderedToBuild => isOrderedToBuild && buildingToPlace != null;
    bool ReachedDestination => Vector3.Distance(transform.position, aiPath.destination) <= 1.5f;
    public bool IsSelected => Selected.activeSelf;

    GridBuildingSystem gridBuildingSystem;
    PlacedObjectTypeSO buildingToPlace;
    AIPath aiPath;
    Damageable attackTarget;
    bool isOrderedToBuild;

    float lastAttackTime;
    const float timeBetweenAttacks = 1f;

    void Awake() {
      gridBuildingSystem = FindObjectOfType<GridBuildingSystem>();
      aiPath = GetComponent<AIPath>();
      Selected = transform.Find("Selected").gameObject;
    }

    public void MoveTo(Vector3 position) {
      attackTarget = null;
      buildingToPlace = null;
      isOrderedToBuild = false;

      aiPath.destination = position;
    }

    public void Build(PlacedObjectTypeSO buildingType, Vector3 position) {
      attackTarget = null;

      buildingToPlace = buildingType;
      isOrderedToBuild = true;
      aiPath.destination = position;
    }

    public void Attack(Damageable target) {
      buildingToPlace = null;
      isOrderedToBuild = false;

      attackTarget = target;
      aiPath.destination = target.Position;
    }

    void Update() {
      if (ReachedDestination) {
        if (HasTarget) {
          if (IsTimeToAttack) {
            Attack();
          }
        }
        else if (IsOrderedToBuild) {
          Build();
        }
      }
    }

    void Attack() {
      attackTarget?.TakeDamage(5);
      lastAttackTime = Time.time;
    }

    void Build() {
      gridBuildingSystem.Build(aiPath.destination, buildingToPlace);
      buildingToPlace = null;
      isOrderedToBuild = false;
    }
  }
}