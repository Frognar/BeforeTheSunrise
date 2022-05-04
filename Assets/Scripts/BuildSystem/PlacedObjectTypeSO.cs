using System.Collections.Generic;
using UnityEngine;

namespace bts {
  [CreateAssetMenu()]
  public class PlacedObjectTypeSO : ScriptableObject {
    public string objectName;
    public Transform prefab;
    public Transform ghost;
    public int width;
    public int height;
    public Affiliation objectAffiliation;
    public Type objectType;

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
