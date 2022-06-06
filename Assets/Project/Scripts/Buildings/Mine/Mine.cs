using System;
using System.Collections.Generic;
using UnityEngine;

namespace bts {
  public class Mine : Building {
    [SerializeField] SelectablesEventChannel selectablesEventChannel;
    [SerializeField] VoidEventChannel onDayStarted;
    [SerializeField] GemstoneStorage storage;
    [SerializeField] int mineAmount;
    [SerializeField] List<SelectMineGemTypeUICommandData> selectMineGemTypeUICommandsData;
    [SerializeField] MeshRenderer meshRenderer;
    GemstoneType gemstoneType;
    bool isSet;

    protected override IEnumerable<UICommand> CreateUICommands() {
      foreach (SelectMineGemTypeUICommandData data in selectMineGemTypeUICommandsData) {
        yield return new SelectMineGemTypeUICommand(mine: this, data);
      }

      foreach (UICommand command in base.CreateUICommands()) {
        yield return command;
      }
    }

    public void SetGemstoneType(GemstoneType type, GemstoneDictionary cost, Material material) {
      if (isSet == false && storage.CanAfford(cost)) {
        gemstoneType = type;
        storage.Discard(cost);
        isSet = true;
        UICommands = base.CreateUICommands();
        selectablesEventChannel.InvokeOnRefresh(this);
        meshRenderer.material = material;
      }
    }

    void OnEnable() {
      onDayStarted.OnEventInvoked += MineGem;
    }

    void OnDisable() {
      onDayStarted.OnEventInvoked -= MineGem;
    }

    void MineGem(object sender, EventArgs e) {
      if (isSet) {
        storage.Store(gemstoneType, mineAmount);
      }
    }
  }
}
