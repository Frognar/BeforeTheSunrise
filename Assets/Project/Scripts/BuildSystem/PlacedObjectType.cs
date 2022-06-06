using System.Collections.Generic;
using UnityEngine;

namespace bts {
  [CreateAssetMenu()]
  public class PlacedObjectType : ScriptableObject {
    public string objectName;
    public string objectDescription;
    public Transform prefab;
    public int width;
    public int height;
    public Affiliation objectAffiliation;
    public Type objectType;
    public CustomPlacedObjectData customData;
    [Tooltip("0 = unlimited")] public int limit;
    public IntAsset placedCount;
    
    public bool CanPlace() {
      return limit == 0 || placedCount.value < limit;
    }

    public List<Vector3Int> GetGridPositions(Vector3Int offset) {
      List<Vector3Int> gridPositions = new List<Vector3Int>();
      for (int x = 0; x < width; x++) {
        for (int z = 0; z < height; z++) {
          gridPositions.Add(offset + new Vector3Int(x, 0, z));
        }
      }

      return gridPositions;
    }
  }
}
