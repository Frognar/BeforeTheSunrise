using UnityEngine;

namespace bts {
  [CreateAssetMenu(fileName = "Attack Speed Upgrade Command Data", menuName = "UICommands/Attack Speed Upgrade Command Data")]
  public class AttackSpeedUpgradeUICommandData : UnitStatsUpgradeUICommandData {
    [field: SerializeField] public float Upgrade { get; private set; }

    protected override void UpgradeStats() {
      unitStats.timeBetweenAttacks -= Upgrade;
      if (unitStats.timeBetweenAttacks < 0.1f) {
        unitStats.timeBetweenAttacks = 0.1f;
      }
    }
  }
}