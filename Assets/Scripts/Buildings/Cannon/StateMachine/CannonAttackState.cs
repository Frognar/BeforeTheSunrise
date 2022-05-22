using UnityEngine;

namespace bts {
  public class CannonAttackState : CannonBaseState {
    bool HasTarget => StateMachine.Context.Target != null && (StateMachine.Context.Target as Object) != null;
    bool IsTargetInRange => Vector3.Distance(StateMachine.Context.Target.Position, StateMachine.Context.Position) <= StateMachine.Context.Range;
    float lastAttackTime;

    public CannonAttackState(StateMachine<Cannon> stateMachine, StateFactory<Cannon> factory)
      : base(stateMachine, factory) {
    }

    bool IsTimeToAttack => lastAttackTime + StateMachine.Context.TimeBetweenAttacks <= Time.time;

    public override void UpdateState() {
      if (CheckSwitchState()) {
        return;
      }

      if (HasTarget) {
        if (IsTargetInRange) {
          if (IsTimeToAttack) {
            if (StateMachine.Context.CanAfford(StateMachine.Context.EnergyPerAttack)) {
              lastAttackTime = Time.time;
              StateMachine.Context.UseEnergy(StateMachine.Context.EnergyPerAttack);
              StateMachine.Context.Target.TakeDamage(StateMachine.Context.Damage);
              if (StateMachine.Context.Target.IsDead) {
                StateMachine.Context.Target = null;
                StateMachine.SwitchState(Factory.GetState(nameof(CannonIdleState)));
              }
            }
          }
        }
        else {
          StateMachine.Context.Target = null;
          StateMachine.SwitchState(Factory.GetState(nameof(CannonIdleState)));
        }
      }
    }
  }
}
