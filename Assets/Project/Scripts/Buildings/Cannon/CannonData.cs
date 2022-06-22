using UnityEngine;

namespace bts {
  [CreateAssetMenu(menuName = "Buildings/Data/Cannon", fileName = "CannonData")]
  public class CannonData : CustomBuildingData, Ranged {
    [field: SerializeField] public float Range { get; private set; }
    public float damage;
    public float energyPerAttack;
    public float maxEnergy;
    public float timeBetweenAttacks;
    public ElectricArcVFXConfiguration electricArcConfig;
    public AudioClipsGroup attackSFX;
    public AudioConfiguration audioConfig;
  }
}
