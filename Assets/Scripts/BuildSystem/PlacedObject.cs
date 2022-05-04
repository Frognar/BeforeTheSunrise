using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bts {
  public class PlacedObject : MonoBehaviour {
    static readonly WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
    
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
      _ = StartCoroutine(DestroySelfAfterDelay());
    }

    IEnumerator DestroySelfAfterDelay() {
      obstacle.enabled = false;
      yield return waitForFixedUpdate;
      AstarPath.active.UpdateGraphs(obstacle.bounds);
      yield return waitForFixedUpdate;
      obstacle.enabled = true;
      yield return waitForFixedUpdate;
      gameObject.transform.position = new Vector3(-10000, -10000, -10000);
      yield return waitForFixedUpdate;
      Destroy(gameObject);
    }
  }
}
