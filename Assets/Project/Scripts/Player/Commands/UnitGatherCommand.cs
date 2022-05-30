namespace bts {
  public class UnitGatherCommand : Command { 
    Unit Unit { get; }
    Gemstone Gemstone { get; }

    public UnitGatherCommand(Unit unit, Gemstone gemstone) {
      Unit = unit;
      Gemstone = gemstone;
    }

    public void Execute() {
      Unit.IsOrderedToGather = true;
      Unit.TargerGemstone = Gemstone;
    }
  }
}
