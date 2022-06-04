using System.Collections.Generic;
using UnityEngine;

namespace bts {
  public class Base : Building {
    [SerializeField] List<UnitStatsUpgradeUICommandData> statsUpgradeUICommandsDatas;

    protected override IEnumerable<UICommand> CreateUICommands() {
      foreach (UnitStatsUpgradeUICommandData data in statsUpgradeUICommandsDatas) {
        yield return new UnitStatsUpgradeUICommand(data);
      }

      foreach (UICommand command in base.CreateUICommands()) {
        yield return command;
      }
    }
  }
}
