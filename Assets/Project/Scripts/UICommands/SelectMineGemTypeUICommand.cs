namespace bts {
  public class SelectMineGemTypeUICommand : UICommand {
    Mine Mine { get; }
    SelectMineGemTypeUICommandData Data { get; }

    public SelectMineGemTypeUICommand(Mine mine, UICommandData commandData) : base(commandData) {
      Mine = mine;
      Data = commandData as SelectMineGemTypeUICommandData;
    }
    
    public override void Execute() {
      Mine.SetGemstoneType(Data.GemstoneType, Data.TooltipData.Gemstones, Data.Material);
    }
  }
}
