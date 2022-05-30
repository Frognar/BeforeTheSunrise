namespace bts {
  public class HealerStopCommand : Command {
    Healer Healer { get; }

    public HealerStopCommand(Healer healer) {
      Healer = healer;
    }

    public void Execute() {
      Healer.IsOrderedToStop = true;
      Healer.Target = null;
    }
  }
}
