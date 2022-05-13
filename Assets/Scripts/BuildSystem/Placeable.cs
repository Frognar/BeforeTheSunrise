using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

namespace bts {
  [RequireComponent(typeof(Collider))]
  public abstract class Placeable : MonoBehaviour {
    static Transform parent;

    public static Placeable Create(Vector3 worldPosition, Vector3Int origin, PlacedObjectTypeSO placedObjectType, GridBuildingSystem gridBuildingSystem) {
      if (parent == null) {
        parent = new GameObject("Buildings|Obstacles").transform;
      }

      Transform placedObjectTransform = Instantiate(placedObjectType.prefab, worldPosition, Quaternion.identity, parent);
      Placeable placedObject = placedObjectTransform.GetComponent<Placeable>();
      placedObject.placedObjectType = placedObjectType;
      placedObject.origin = origin;
      placedObject.gridBuildingSystem = gridBuildingSystem;
      return placedObject;
    }

    protected GridBuildingSystem gridBuildingSystem;
    protected PlacedObjectTypeSO placedObjectType;
    protected Vector3Int origin;
    protected Collider obstacle;

    public List<Vector3Int> GetGridPositions() {
      return placedObjectType.GetGridPositions(origin);
    }

    public virtual void DestroySelf() {
      Bounds b = obstacle.bounds;
      transform.position = new Vector3(-10000, -10000, -10000);
      Destroy(gameObject, 0.1f);
      AstarPath.active.UpdateGraphs(new GraphUpdateObject(b));
    }

    void Awake() {
      obstacle = GetComponent<Collider>();
      AstarPath.active.UpdateGraphs(obstacle.bounds);
    }
  }
}
