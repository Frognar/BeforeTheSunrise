using System;
using System.Collections.Generic;
using bts.Gemstones;
using fro.ValueAssets;
using UnityEngine;

namespace bts {
  public class Mine : Building {
    [SerializeField] VoidEventChannel onDayStarted;
    [SerializeField] int mineAmount;
    int MineAmount => mineAmount * (int)Mathf.Pow(2, BuildingLevel);
    [SerializeField] List<SelectMineGemTypeUICommandData> selectMineGemTypeUICommandsData;
    [SerializeField] MeshRenderer meshRenderer;
    GemstoneType gemstoneType;
    bool isSet;
    [SerializeField] IntAsset mineCount;

    protected override IEnumerable<UICommand> CreateUICommands() {
      if (isSet == false) {
        foreach (SelectMineGemTypeUICommandData data in selectMineGemTypeUICommandsData) {
          yield return new SelectMineGemTypeUICommand(mine: this, data);
        }
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
        SelectablesEventChannel.InvokeOnRefresh(this);
        meshRenderer.material = material;
      }
    }

    void OnEnable() {
      onDayStarted.OnEventInvoked += MineGem;
      mineCount.value += 1;
    }

    void OnDisable() {
      onDayStarted.OnEventInvoked -= MineGem;
      mineCount.value -= 1;
    }

    void MineGem(object sender, EventArgs e) {
      if (isSet) {
        storage.Store(gemstoneType, MineAmount);
      }
    }

    public override Dictionary<DataType, object> GetData() {
      Dictionary<DataType, object> data = base.GetData();
      if (isSet) {
        data.Add(DataType.GemstoneType, gemstoneType);
        data.Add(DataType.GatherAmount, MineAmount);
      }

      return data;
    }

    protected override Dictionary<DataType, object> GetDataTypesOnUpgrage() {
      Dictionary<DataType, object> data = base.GetDataTypesOnUpgrage();
      if (isSet) {
        data.Add(DataType.GemstoneType, gemstoneType);
        data.Add(DataType.GatherAmount, MineAmount);
      }

      return data;
    }

    public override bool IsSameAs(Selectable other) {
      return other is Mine m
        && BuildingLevel == m.BuildingLevel
        && isSet == m.isSet
        && gemstoneType == m.gemstoneType;
    }
  }
}
