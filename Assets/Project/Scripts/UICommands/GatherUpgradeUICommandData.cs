using bts.Gemstones;
using UnityEngine;

namespace bts {

  [CreateAssetMenu(fileName = "Gather Upgrade Command Data", menuName = "UICommands/Gather Upgrade Command Data")]
  public class GatherUpgradeUICommandData : UnitStatsUpgradeUICommandData {
    [field: SerializeField] public GemstoneDictionary Upgrade { get; private set; }
    
    protected override void UpgradeStats() {
        unitStats.gatherBonuses.Add(Upgrade);
    }
  }
}