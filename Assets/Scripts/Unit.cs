using Pathfinding;
using UnityEngine;

namespace bts {
  public class Unit : MonoBehaviour, Selectable {
    public string Name => "Unit";
    public Transform Transform => transform;
    public Affiliation ObjectAffiliation => Affiliation.Player;
    public Type ObjectType => Type.Unit;
    public GameObject Selected { get; private set; }
    bool HasTarget => target != null && (target as Object) != null;
    bool IsTimeToAttack => lastAttackTime + timeBetweenAttacks < Time.time;
    bool IsOrderedToBuild => isOrderedToBuild && buildingToPlace != null;

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
      ClearGhost();
    }

    void ClearGhost() {
      if (currentGhost != null) {
        Destroy(currentGhost.gameObject);
        currentGhost = null;
      }
    }

    public void MoveTo(Vector3 positon) {
      aiPath.destination = positon;
    }

    void Update() {
      HandleInput();

      if (Vector3.Distance(transform.position, destination) < 2.5f) {
        if (HasTarget && IsTimeToAttack) {
          Attack();
        }
        else if (IsOrderedToBuild) {
          Build();
        }
      }
    }

    void HandleInput() {
      if (isSelected && playerInputs.IsRightBtnDawn) {
        Ray ray = cam.ScreenPointToRay(playerInputs.MouseScreenPosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo)) {
          destination = hitInfo.point;
          MoveTo(destination);
          isOrderedToBuild = false;
          target = null;
          ClearGhost();
          if (buildingToPlace != null) {
            isOrderedToBuild = true;
          }
          else if (hitInfo.transform.TryGetComponent(out Damageable damageable) && damageable.ObjectAffiliation != Affiliation.Player) {
            target = damageable;
          }
        }
      }
    }

    void Attack() {
      target?.TakeDamage(5);
      lastAttackTime = Time.time;
    }

    void Build() {
      gridBuildingSystem.Build(destination, buildingToPlace);
      buildingToPlace = null;
      isOrderedToBuild = false;
    }

    public void SetBuildingToBuild(PlacedObjectTypeSO buildingType) {
      if (isSelected) {
        buildingToPlace = buildingType;
        ClearGhost();
        currentGhost = Instantiate(buildingType.ghost).GetComponent<GhostObject>();
      }
    }

    public void ClearBuildingToBuild() {
      buildingToPlace = null;
      ClearGhost();
    }
  }
}