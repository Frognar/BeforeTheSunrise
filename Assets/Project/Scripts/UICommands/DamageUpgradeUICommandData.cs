using UnityEngine;

namespace bts {
  [CreateAssetMenu(fileName = "Damage Upgrade Command Data", menuName = "UICommands/Damage Upgrade Command Data")]
  public class DamageUpgradeUICommandData : UnitStatsUpgradeUICommandData {
    [field: SerializeField] public float Upgrade { get; private set; }

    protected override void UpgradeStats() {
      unitStats.damageAmount += Upgrade * Mathf.Pow(2, upgradeLevel);
    }
  }
}