using System.Collections.Generic;
using UnityEngine;

namespace fro.BuildingSystem {
  [CreateAssetMenu(fileName="Object Data", menuName = "Building System/Object Data")]
  public class PlacedObjectData : ScriptableObject {
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public int Width { get; private set; }
    [field: SerializeField] public int Height { get; private set; }
    [field: SerializeField] public PlacedObject Prefab { get; private set; }
    
    public List<GridCords> GetGridPositions(GridCords origin) {
      List<GridCords> positions = new List<GridCords>();
      for (int x = 0; x < Width; x++) {
        for (int z = 0; z < Height; z++) {
          positions.Add(new GridCords(origin.X + x, origin.Z + z));
        }
      }
      
      return positions;
    }
  }
}
