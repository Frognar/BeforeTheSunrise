using bts.Gemstones;
using UnityEngine;

namespace bts {
  public abstract class UnitStatsUpgradeUICommandData : UICommandData {
    [SerializeField] GemstoneStorage storage;
    [SerializeField] protected UnitStats unitStats;
    [SerializeField] string tooltipDescription;
    TooltipData tooltipData;
    public override TooltipData TooltipData => tooltipData;
    [SerializeField] int maxUpgradeLevel;
    protected int upgradeLevel;
    [SerializeField] GemstoneDictionary baseCost;
    public GemstoneDictionary Cost => baseCost * (int)Mathf.Pow(2, upgradeLevel);

    protected bool CanUpgrade() {
      return upgradeLevel < maxUpgradeLevel && storage.CanAfford(Cost);
    }

    public void BuyUpgrade() {
      if (CanUpgrade()) {
        storage.Discard(Cost);
        UpgradeStats();
        upgradeLevel++;
        tooltipData = new(string.Empty, tooltipDescription, Cost);
      }
    }

    protected abstract void UpgradeStats();

    void OnEnable() {
      upgradeLevel = 0;
      tooltipData = new(string.Empty, tooltipDescription, Cost);
    }
  }
}