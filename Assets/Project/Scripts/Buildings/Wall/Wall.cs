namespace bts {
  public class Wall : Building {
    public override bool IsSameAs(Selectable other) {
      return other is Wall w && BuildingLevel == w.BuildingLevel;
    }
  }
}
