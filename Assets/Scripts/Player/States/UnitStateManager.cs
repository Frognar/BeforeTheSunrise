using System;
using System.Collections.Generic;
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

    public bool IsIdle { get; set; }

    [SerializeField] GemstoneStorage gemstoneStorage;
    public GemstoneStorage GemstoneStorage => gemstoneStorage;
    public float GatherRange => 3f;
    public float TimeBetweenGathers => 1f;
    public Dictionary<GemstoneType, int> GatherBonuses { get; private set; }
    public bool IsOrderedToGather { get; set; }
    public Gemstone TargerGemstone { get; set; }

    public float BuildRange => 3.5f;
    public bool IsOrderedToBuild { get; set; }
    public PlacedObjectTypeSO BuildingToPlace { get; set; }

    public float StopDistance => 1f;
    public bool IsOrderedToMove { get; set; }
    public Vector3 Destination { get; set; }

    public int DamageAmount => 5;
    public float TimeBetweenAttacks => 1f;
    public float AttackRange => 4f;
    public bool IsOrderedToAttack { get; set; }
    public Damageable Target { get; set; }

    UnitBaseState currentState;
    UnitStateFactory stateFactory;

    void Awake() {
      GatherBonuses = new Dictionary<GemstoneType, int>();
      foreach (GemstoneType type in Enum.GetValues(typeof(GemstoneType))) {
        GatherBonuses[type] = 1;
      }

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
  }
}