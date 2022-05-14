using UnityEngine;

namespace bts {
  public class UnitAttackState : UnitBaseState {
    float lastAttackTime;
    bool IsTimeToAttack => lastAttackTime + Context.TimeBetweenAttacks <= Time.time;
    bool InAttackRange => Vector3.Distance(Context.CurrentPosition, Context.Target.Position) <= Context.AttackRange;
    float prevStopDistance;
    
    public UnitAttackState(UnitStateManager context, UnitStateFactory factory)
      : base(context, factory) {
    }

    public override void EnterState() {
      prevStopDistance = Context.AiPath.endReachedDistance;
      Context.AiPath.endReachedDistance = Context.AttackRange - 2f;
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
            SwitchState(StateFactory.Idle);
          }
    }

    public override void ExitState() {
      Context.AiPath.destination = Context.CurrentPosition;
      Context.AiPath.endReachedDistance = prevStopDistance;
    }
  }
}