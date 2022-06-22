using fro.ValueAssets;
using UnityEngine;

namespace bts {
  [CreateAssetMenu(menuName = "Buildings/Data/Drill", fileName = "DrillData")]
  public class DrillData : CustomBuildingData, Limited {
    [field: SerializeField] public int Limit { get; private set; }
    [field: SerializeField] public IntAsset PlacedCount { get; private set; }
  }
}
