using UnityEngine;

namespace bts {
  [CreateAssetMenu(menuName = "Buildings/Data/Aura", fileName = "Aura Data")]
  public class AuraData : CustomBuildingData, Ranged {
    [field: SerializeField] public float Range { get; private set; }
    public float maxEnergy;
    public float energyPerDevicePerSecond;
  }
}
