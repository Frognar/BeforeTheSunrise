using Pathfinding;
using UnityEngine;

namespace bts {
  public class UnitStateManager : MonoBehaviour, Selectable {
    public string Name => "Unit";
    public Transform Transform => transform;
    public Affiliation ObjectAffiliation => Affiliation.Player;
    public Type ObjectType => Type.Unit;
    public GameObject Selected { get; private set; }
    public bool IsSelected => Selected.activeSelf;
    public GridBuildingSystem GridBuildingSystem { get; private set; }
    public AIPath AiPath { get; private set; }
    public AIDestinationSetter AIDestinationSetter { get; private set; }
    public Vector3 CurrentPosition => transform.position;

    public float BuildRange => 2f;
    public bool IsOrderedToBuild { get; set; }
    public PlacedObjectTypeSO BuildingToPlace { get; set; }

    public float StopDistance => 1f;
    public bool IsOrderedToMove { get; set; }
    public bool ReachedDestination => Vector3.Distance(transform.position, Destination) <= AiPath.endReachedDistance;
    public Vector3 Destination { get; set; }

    public int DamageAmount => 5;
    public float TimeBetweenAttacks => 1f;
    public float AttackRange => 1.8f;
    public bool IsOrderedToAttack { get; set; }
    public Damageable Target { get; set; }

    UnitBaseState currentState;
    UnitStateFactory stateFactory;

    void Awake() {
      GridBuildingSystem = FindObjectOfType<GridBuildingSystem>();
      AiPath = GetComponent<AIPath>();
      AIDestinationSetter = GetComponent<AIDestinationSetter>();
      Selected = transform.Find("Selected").gameObject;
      stateFactory = new UnitStateFactory(context: this);
      SwitchState(stateFactory.Idle);
    }

    public void SwitchState(UnitBaseState newState) {
      currentState = newState;
      currentState.EnterState();
    }

    void Update() {
      currentState.UpdateState();
    }

    public void SetMoveOrder(Vector3 destination) {
      IsOrderedToMove = true;
      Destination = destination;
    }

    public void SetBuildOrder(PlacedObjectTypeSO buildingType, Vector3 position) {
      IsOrderedToBuild = true;
      BuildingToPlace = buildingType;
      Destination = position;
    }

    public void SetAttackOrder(Damageable target) {
      IsOrderedToAttack = true;
      Target = target;
    }
  }
}