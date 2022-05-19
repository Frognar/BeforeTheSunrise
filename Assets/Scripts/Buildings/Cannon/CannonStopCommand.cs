namespace bts {
  public class CannonStopCommand : Command {
    Cannon Cannon { get; }

    public CannonStopCommand(Cannon cannon) {
      Cannon = cannon;
    }

    public void Execute() {
      Cannon.IsOrderedToStop = true;
      Cannon.Target = null;
    }
  }
}
