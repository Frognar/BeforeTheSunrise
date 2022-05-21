namespace bts {
  public class DemolishUICommand : UICommand {
    PlacedObject Building { get; }
    GemstoneStorage GemstoneStorage { get; }
    float RefundRate { get; }

    public DemolishUICommand(DemolishUICommandData data, PlacedObject building, GemstoneStorage gemstoneStorage)
      : base(data) {
      Building = building;
      GemstoneStorage = gemstoneStorage;
      RefundRate = data.RefundRate;
    }

    public override void Execute() {
      GemstoneStorage.Refund(Building.BuildingCosts, RefundRate);
      Building.DestroySelf();
    }
  }
}
