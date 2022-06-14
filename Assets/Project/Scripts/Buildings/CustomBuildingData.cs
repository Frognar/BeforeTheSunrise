using bts.Gemstones;
using fro.ValueAssets;
using UnityEngine;

namespace bts {
  [CreateAssetMenu(menuName = "Buildings/Data/Custom", fileName = "CustomBuildingData")]
  public class CustomBuildingData : ScriptableObject {
    public Sprite icon;
    public GemstoneDictionary buildingCosts;
    public float healthAmount;
    public Mesh ghostMesh;
    [Tooltip("0 = unlimited")] public int limit;
    public IntAsset placedCount;
    
    public bool CanPlace() {
      return limit == 0 || placedCount.value < limit;
    }
  }
}
