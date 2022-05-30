namespace bts {
  public class HealerHealCommand : Command {
    Healer Healer { get; }
    Damageable Target { get; }

    public HealerHealCommand(Healer healer, Damageable target) {
      Healer = healer;
      Target = target;
    }

    public void Execute() {
      if (Target != null && (Target as UnityEngine.Object) != null) {
        Healer.IsOrderedToHeal = true;
        Healer.Target = Target;
      }
    }
  }
}
