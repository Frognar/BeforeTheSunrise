using UnityEngine;

namespace bts {
  public class UnitAttackState : UnitBaseState {
    float lastAttackTime;
    public bool HasTarget => StateMachine.Context.Target != null && (StateMachine.Context.Target as Object) != null;
    bool IsTimeToAttack => lastAttackTime + StateMachine.Context.TimeBetweenAttacks <= Time.time;
    bool InAttackRange => Vector3.Distance(StateMachine.Context.CurrentPosition, StateMachine.Context.Target.Position) <= StateMachine.Context.AttackRange;
    float prevStopDistance;

    public UnitAttackState(StateMachine<Unit> stateMachine, StateFactory<Unit> factory)
      : base(stateMachine, factory) {
    }

    public override void EnterState() {
      if (HasTarget) {
        prevStopDistance = StateMachine.Context.AiPath.endReachedDistance;
        StateMachine.Context.AiPath.endReachedDistance = StateMachine.Context.AttackRange - 2f;
        StateMachine.Context.IsOrderedToAttack = false;
        StateMachine.Context.AIDestinationSetter.target = StateMachine.Context.Target.Transform;
      }
      else if (!CheckSwitchState()) {
        StateMachine.SwitchState(Factory.GetState(nameof(UnitIdleState)));
      }
    }

    public override void UpdateState() {
      if (CheckSwitchState()) {
        return;
      }

      if (HasTarget) {
        if (InAttackRange) {
          if (IsTimeToAttack) {
            Attack();
          }
        }
      }
      else {
        StateMachine.SwitchState(Factory.GetState(nameof(UnitIdleState)));
      }
    }

    void Attack() {
      lastAttackTime = Time.time;
      StateMachine.Context.Target.TakeDamage(StateMachine.Context.DamageAmount);
      if (StateMachine.Context.Target.IsDead) {
        StateMachine.Context.Target = null;
        StateMachine.Context.AIDestinationSetter.target = null;
        StateMachine.SwitchState(Factory.GetState(nameof(UnitIdleState)));
      }
    }

    public override void ExitState() {
      StateMachine.Context.AiPath.destination = StateMachine.Context.CurrentPosition;
      StateMachine.Context.AiPath.endReachedDistance = prevStopDistance;
    }
  }
}