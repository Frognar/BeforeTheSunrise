namespace bts {
  public class DemolishUICommand : UICommand {
    Building Building { get; }
    GemstoneStorage GemstoneStorage { get; }
    float RefundRate { get; }

    public DemolishUICommand(DemolishUICommandData data, Building building)
      : base(data) {
      Building = building;
      GemstoneStorage = data.Storage;
      RefundRate = data.RefundRate;
    }

    public override void Execute() {
      GemstoneStorage.Refund(Building.BuildingCosts, RefundRate);
      Building.DestroySelf();
    }
  }
}
