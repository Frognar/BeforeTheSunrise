using fro.ValueAssets;
using UnityEngine;

namespace bts {
  [CreateAssetMenu(menuName = "Buildings/Data/Mine", fileName = "MineData")]
  public class MineData : CustomBuildingData, Limited {
    [field: SerializeField] public int Limit { get; private set; }
    [field: SerializeField] public IntAsset PlacedCount { get; private set; }
  }
}
