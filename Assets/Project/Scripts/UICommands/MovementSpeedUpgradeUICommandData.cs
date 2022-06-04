using UnityEngine;

namespace bts {
  [CreateAssetMenu(fileName = "Movement Speed Upgrade Command Data", menuName = "UICommands/Movement Speed Upgrade Command Data")]
  public class MovementSpeedUpgradeUICommandData : UnitStatsUpgradeUICommandData {
    [field: SerializeField] public float Upgrade { get; private set; }

    protected override void UpgradeStats() {
      unitStats.MovementSpeed += Upgrade;
    }
  }
}