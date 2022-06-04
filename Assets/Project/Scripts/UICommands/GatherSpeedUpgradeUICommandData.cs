using UnityEngine;

namespace bts {
  [CreateAssetMenu(fileName = "Gather Speed Upgrade Command Data", menuName = "UICommands/Gather Speed Upgrade Command Data")]
  public class GatherSpeedUpgradeUICommandData : UnitStatsUpgradeUICommandData {
    [field: SerializeField] public float Upgrade { get; private set; }

    protected override void UpgradeStats() {
      unitStats.timeBetweenGathers -= Upgrade;
      if (unitStats.timeBetweenGathers < 0.1f) {
        unitStats.timeBetweenGathers = 0.1f;
      }
    }
  }
}