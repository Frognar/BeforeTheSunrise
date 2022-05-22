using UnityEngine;

namespace bts {
  public class UnitAttackCommand : Command {
    Unit Unit { get; }
    Damageable Target { get; }

    public UnitAttackCommand(Unit unit, Damageable target) {
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
