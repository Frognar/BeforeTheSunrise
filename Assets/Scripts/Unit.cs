using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.UI;

namespace bts {
  public class Unit : MonoBehaviour, Selectable {
    public string Name => "Unit";
    public Transform Transform => transform;
    public Affiliation ObjectAffiliation => Affiliation.Player;
    public Type ObjectType => Type.Unit;
    public GameObject Selected { get; private set; }

    GridBuildingSystem gridBuildingSystem;
    PlacedObjectTypeSO buildingToPlace;
    PlayerInputs playerInputs;
    Camera cam;
    AIPath aiPath;
    Vector3 destination;
    Damageable target;
    bool isSelected;
    bool isOrderedToBuild;
    GhostObject currentGhost;

    float lastAttackTime;
    const float timeBetweenAttacks = 1f;

    void Awake() {
      gridBuildingSystem = FindObjectOfType<GridBuildingSystem>();
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
      buildingToPlace = null;
      Debug.Log("Deselect");
      if (currentGhost != null) {
        Destroy(currentGhost.gameObject);
        currentGhost = null;
      }
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
            if (buildingToPlace != null) {
              destination = hitInfo.point;
              isOrderedToBuild = true;
              MoveTo(destination);
              if (currentGhost != null) {
                Destroy(currentGhost.gameObject);
                currentGhost = null;
              }
            }
            else {
              MoveTo(hitInfo.point);
            }
          }
        }
      }

      if (isOrderedToBuild && buildingToPlace != null) {
        if (Vector3.Distance(transform.position, destination) < 2.5f) {
          gridBuildingSystem.Build(destination, buildingToPlace);
          buildingToPlace = null;
          isOrderedToBuild = false;
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

    public void SetBuildingToBuild(PlacedObjectTypeSO buildingType) {
      if (isSelected) {
        buildingToPlace = buildingType;
        if (currentGhost != null) {
          Destroy(currentGhost.gameObject);
          currentGhost = null;
        }

        currentGhost = Instantiate(buildingType.ghost).GetComponent<GhostObject>();
      }
    }

    public void ClearBuildingToBuild() {
      buildingToPlace = null;
      if (currentGhost != null) {
        Destroy(currentGhost.gameObject);
        currentGhost = null;
      }
    }
  }
}