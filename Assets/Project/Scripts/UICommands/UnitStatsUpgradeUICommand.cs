namespace bts {
  public class UnitStatsUpgradeUICommand : UICommand {
    public UnitStatsUpgradeUICommandData Data { get; }

    public UnitStatsUpgradeUICommand(UICommandData data)
      : base(data) {
      Data = data as UnitStatsUpgradeUICommandData;
    }

    public override void Execute() {
      Data.BuyUpgrade();
    }
  }
}