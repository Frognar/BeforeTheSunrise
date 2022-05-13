namespace bts {
  public class UnitGatherCommand : Command { 
    UnitStateManager Unit { get; }
    Gemstone Gemstone { get; }

    public UnitGatherCommand(UnitStateManager unit, Gemstone gemstone) {
      Unit = unit;
      Gemstone = gemstone;
    }

    public void Execute() {
      Unit.IsOrderedToGather = true;
      Unit.TargerGemstone = Gemstone;
    }
  }
}
