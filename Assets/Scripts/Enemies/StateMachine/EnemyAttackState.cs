using UnityEngine;

namespace bts {
  public class EnemyAttackState : EnemyBaseState {
    float lastAttackTime;
    public bool HasTarget => StateMachine.Context.Target != null && (StateMachine.Context.Target as Object) != null;
    bool IsTimeToAttack => lastAttackTime + StateMachine.Context.EnemyData.TimeBetweenAttacks <= Time.time;
    bool InAttackRange => Vector3.Distance(StateMachine.Context.Position, StateMachine.Context.Target.Position) <= StateMachine.Context.EnemyData.AttackRange;

    public EnemyAttackState(StateMachine<Enemy> stateMachine, StateFactory<Enemy> factory)
      : base(stateMachine, factory) {
    }

    public override void EnterState() {
      if (HasTarget) {
        StateMachine.Context.AIDestinationSetter.target = StateMachine.Context.Target.Transform;
      }
      else {
        StateMachine.SwitchState(Factory.GetState(nameof(EnemyLookingForTargetState)));
      }
    }

    public override void UpdateState() {
      if (HasTarget) {
        if (InAttackRange) {
          if (IsTimeToAttack) {
            Attack();
          }
        }
      }
      else {
        StateMachine.SwitchState(Factory.GetState(nameof(EnemyLookingForTargetState)));
      }
    }

    void Attack() {
      lastAttackTime = Time.time;
      StateMachine.Context.Target.TakeDamage(StateMachine.Context.EnemyData.Damage);
      if (StateMachine.Context.Target.IsDead) {
        StateMachine.Context.Target = null;
        StateMachine.Context.AIDestinationSetter.target = null;
        StateMachine.SwitchState(Factory.GetState(nameof(EnemyLookingForTargetState)));
      }
    }
  }
}
