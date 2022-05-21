using UnityEngine;

namespace bts {
  public class CannonAttackState : CannonBaseState {
    bool HasTarget => Context.Target != null;
    bool IsTargetInRange => Vector3.Distance(Context.Target.Position, Context.Position) <= Context.Range;
    float lastAttackTime;
    bool IsTimeToAttack => lastAttackTime + Context.TimeBetweenAttacks <= Time.time;

    public CannonAttackState(Cannon context, CannonStateFactory factory)
      : base(context, factory) {
    }

    public override void UpdateState() {
      if (CheckSwitchState()) {
        return;
      }

      if (HasTarget) {
        if (IsTargetInRange) {
          if (IsTimeToAttack) {
            if (ElectricContext.CanAfford(Context.EnergyPerAttack)) {
              lastAttackTime = Time.time;
              ElectricContext.Use(Context.EnergyPerAttack);
              Context.Target.TakeDamage(Context.Damage);
              if (Context.Target.IsDead) {
                Context.Target = null;
                SwitchState(Factory.Idle);
              }
            }
          }
        }
        else {
          Context.Target = null;
          SwitchState(Factory.Idle);
        }
      }
    }
  }
}
