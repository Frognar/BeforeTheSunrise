using UnityEngine;

namespace bts {
  [CreateAssetMenu(menuName = "Buildings/Data/Healer", fileName = "HealerData")]
  public class HealerData : CustomBuildingData, Ranged {
    [field: SerializeField] public float Range { get; private set; }
    public float healAmount;
    public float energyPerHeal;
    public float maxEnergy;
    public AudioClipsGroup healSFX;
    public AudioConfiguration audioConfig;
    public LaserConfiguration laserConfig;
  }
}
