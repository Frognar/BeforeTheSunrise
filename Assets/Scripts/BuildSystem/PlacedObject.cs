using System.Collections.Generic;
using UnityEngine;

namespace bts {
  public class PlacedObject : MonoBehaviour {
    public static PlacedObject Create(Vector3 worldPosition, Vector3Int origin, PlacedObjectTypeSO placedObjectType) {
      Transform placedObjectTransform = Instantiate(placedObjectType.prefab, worldPosition, Quaternion.identity);
      PlacedObject placedObject = placedObjectTransform.GetComponent<PlacedObject>();
      placedObject.placedObjectType = placedObjectType;
      placedObject.origin = origin;
      return placedObject;
    }

    PlacedObjectTypeSO placedObjectType;
    Vector3Int origin;
    Collider obstacle;

    public List<Vector3Int> GetGridPositions() {
      return placedObjectType.GetGridPositions(origin);
    }

    void Start() {
      obstacle = GetComponent<Collider>();
      obstacle.enabled = true;
      AstarPath.active.UpdateGraphs(obstacle.bounds);
    }

    public void DestroySelf() {
      obstacle.enabled = false;
      AstarPath.active.UpdateGraphs(obstacle.bounds);
      Destroy(gameObject);
    }
  }
}
