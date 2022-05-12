using UnityEngine;

namespace bts {
  public class UnitAttackState : UnitBaseState {
    float lastAttackTime;
    bool IsTimeToAttack => lastAttackTime + Context.TimeBetweenAttacks <= Time.time;
    bool InAttackRange => Vector3.Distance(Context.CurrentPosition, Context.Target.Position) <= Context.AttackRange;

    public UnitAttackState(UnitStateManager context, UnitStateFactory factory)
      : base(context, factory) {
    }

    public override void EnterState() {
      Context.IsOrderedToAttack = false;
      Context.AIDestinationSetter.target = Context.Target.Transform;
    }

    public override void UpdateState() {
      if (CheckSwitchState()) {
        return;
      }

      if (InAttackRange) {
        if (IsTimeToAttack) {
          Attack();
        }
      }
    }

    void Attack() {
      lastAttackTime = Time.time;
      Context.Target.TakeDamage(Context.DamageAmount);
      if (Context.Target.IsDead) {
        Context.Target = null;
        Context.AIDestinationSetter.target = null;
        Context.Destination = Context.CurrentPosition;
        SwitchState(StateFactory.Idle);
      }
    }
  }
}