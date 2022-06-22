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
  }
}
