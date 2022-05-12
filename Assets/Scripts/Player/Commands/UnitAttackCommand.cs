using UnityEngine;

namespace bts {
  public class UnitAttackCommand : Command {
    UnitStateManager Unit { get; }
    Damageable Target { get; }

    public UnitAttackCommand(UnitStateManager unit, Damageable target) {
      Unit = unit;
      Target = target;
    }

    public void Execute() {
      if (Target != null && (Target as Object) != null) {
        Unit.IsOrderedToAttack = true;
        Unit.Target = Target;
      }
    }
  }
}
