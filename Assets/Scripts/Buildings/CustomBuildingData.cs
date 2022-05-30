using UnityEngine;

namespace bts {
  [CreateAssetMenu(menuName = "Buildings/Data/Custom", fileName = "CustomBuildingData")]
  public class CustomBuildingData : CustomPlacedObjectData {
    public GemstoneDictionary buildingCosts;
    public int healthAmount;
    public Mesh ghostMesh;
  }
}
