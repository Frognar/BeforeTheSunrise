using UnityEngine;

namespace bts {
  [CreateAssetMenu(menuName = "Buildings/Data/Aura", fileName = "Aura Data")]
  public class AuraData : CustomBuildingData {
    public float range;
    public float maxEnergy;
    public float energyPerDevicePerSecond;
  }
}
