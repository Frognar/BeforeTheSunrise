using UnityEngine;

namespace bts {
  [CreateAssetMenu(fileName = "Max Health Upgrade Command Data", menuName = "UICommands/Max Health Upgrade Command Data")]
  public class MaxHealthUpgradeUICommandData : UnitStatsUpgradeUICommandData {
    [field: SerializeField] public float Upgrade { get; private set; }

    protected override void UpgradeStats() {
      unitStats.MaxHealth += Upgrade * Mathf.Pow(2, upgradeLevel);
    }
  }
}