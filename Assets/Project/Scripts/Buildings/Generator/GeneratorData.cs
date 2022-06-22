using UnityEngine;

namespace bts {
  [CreateAssetMenu(menuName = "Buildings/Data/Generator", fileName = "GeneratorData")]
  public class GeneratorData : CustomBuildingData, Ranged {
    [field: SerializeField] public float Range { get; private set; }
    [field: SerializeField] public float EnergyPerSecond { get; private set; }
    [field: SerializeField] public int MaxDevices { get; private set; }
  }
}
