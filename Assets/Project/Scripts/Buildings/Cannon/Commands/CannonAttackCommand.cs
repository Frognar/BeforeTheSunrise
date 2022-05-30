namespace bts {
  public class CannonAttackCommand : Command {
    Cannon Cannon { get; }
    Damageable Target { get; }

    public CannonAttackCommand(Cannon cannon, Damageable target) {
      Cannon = cannon;
      Target = target;
    }

    public void Execute() {
      if (Target != null && (Target as UnityEngine.Object) != null) {
        Cannon.IsOrderedToAttack = true;
        Cannon.Target = Target;
      }
    }
  }
}
