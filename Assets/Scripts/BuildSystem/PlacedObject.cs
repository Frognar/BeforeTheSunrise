using UnityEngine;

namespace bts {
  public abstract class PlacedObject : MonoBehaviour, Placeable, Selectable {
    public Collider Obstacle { get; private set; }
    public Transform Transform => transform;
    public GridBuildingSystem GridBuildingSystem { get; private set; }
    public PlacedObjectTypeSO PlaceableObjectType { get; private set; }
    public Vector3Int Origin { get; private set; }
    public Transform Center { get; private set; }
    public virtual string Name => PlaceableObjectType.name;
    public Type ObjectType => PlaceableObjectType.objectType;
    public Affiliation ObjectAffiliation => PlaceableObjectType.objectAffiliation;
    [field: SerializeField] public GameObject Selected { get; private set; }
    [field: SerializeField] public SelectablesEventChannel SelectablesEventChannel { get; private set; }

    public void Init(GridBuildingSystem gridBuildingSystem, PlacedObjectTypeSO objectType, Vector3Int origin, Transform center) {
      GridBuildingSystem = gridBuildingSystem;
      PlaceableObjectType = objectType;
      Origin = origin;
      Center = center;
    }

    protected virtual void Awake() {
      Obstacle = GetComponent<Collider>();
      AstarPath.active.UpdateGraphs(Obstacle.bounds);
    }

    protected virtual void Start() {
      float selectedScale = Mathf.Max(PlaceableObjectType.width, PlaceableObjectType.height) + 0.5f;
      Selected.transform.localScale = new Vector3(selectedScale, selectedScale, 1f);
      Selected.SetActive(false);
    }

    public virtual void Demolish() {
      Bounds bounds = Obstacle.bounds;
      transform.position = new Vector3(-10000, -10000, -10000);
      AstarPath.active.UpdateGraphs(bounds);
      Destroy(gameObject);
    }

    public virtual void Select() {
      Selected.SetActive(true);
    }

    public virtual void Deselect() {
      Selected.SetActive(false);
    }

    public virtual bool IsSameAs(Selectable other) {
      return false;
    }

    protected virtual void OnDestroy() {
      if (Selected.activeSelf) {
        SelectablesEventChannel.Invoke(this);
      }
    }
  }
}
