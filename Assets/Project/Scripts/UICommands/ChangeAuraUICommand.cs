namespace bts {
  public class ChangeAuraUICommand : UICommand {
    Aura Aura { get; }
    ChangeAuraUICommandData Data { get; }
    int CurrentAuraLevel { get; }
    TooltipData tooltipData;
    public override TooltipData TooltipData => tooltipData;

    public ChangeAuraUICommand(Aura aura, ChangeAuraUICommandData data, int currentAuraLevel)
      : base(data) {
      Aura = aura;
      Data = data;
      CurrentAuraLevel = currentAuraLevel;
      tooltipData = new TooltipData(
        Data.TooltipData.Header,
        Data.TooltipData.Content,
        Data.TooltipData.Gemstones * CurrentAuraLevel
      );
    }

    public override void Execute() {
      if (Data.Storage.CanAfford(Data.BaseCost * CurrentAuraLevel)) {
        Data.Storage.Discard(Data.BaseCost * CurrentAuraLevel);
        Aura.ChangeAura(Data.BoostType);
      }
    }
  }
}
