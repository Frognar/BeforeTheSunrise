using UnityEngine;

namespace bts {
  public class EnemyAttackState : EnemyBaseState {
    float lastAttackTime;
    public bool HasTarget => StateMachine.Context.Target != null && (StateMachine.Context.Target as Object) != null;
    bool IsTimeToAttack => lastAttackTime + StateMachine.Context.EnemyData.TimeBetweenAttacks <= Time.time;
    bool InAttackRange => Vector3.Distance(StateMachine.Context.Position, StateMachine.Context.Target.Position) <= StateMachine.Context.EnemyData.AttackRange;
    float lastCheckPathTime;
    const float checkPathInterval = 2f;
    bool IsTimeToCheckPath => lastCheckPathTime + checkPathInterval <= Time.time;

    public EnemyAttackState(StateMachine<Enemy> stateMachine, StateFactory<Enemy> factory)
      : base(stateMachine, factory) {
    }

    public override void EnterState() {
      if (HasTarget) {
        StateMachine.Context.Pathfinder.SetTarget(StateMachine.Context.Target.Center);
        lastCheckPathTime = Time.time;
      }
      else {
        StateMachine.SwitchState(Factory.GetState(nameof(EnemyLookingForTargetState)));
      }
    }

    public override void UpdateState() {
      if (!HasTarget) {
        StateMachine.SwitchState(Factory.GetState(nameof(EnemyLookingForTargetState)));
        return;
      }

      if (InAttackRange) {
        if (IsTimeToAttack) {
          Attack();
        }
      }
      else if (IsTimeToCheckPath) {
        CheckPath();
      }
    }

    void Attack() {
      lastAttackTime = Time.time;
      StateMachine.Context.Target.TakeDamage(StateMachine.Context.EnemyData.Damage);
      if (StateMachine.Context.Target.IsDead) {
        StateMachine.Context.Target = null;
        StateMachine.SwitchState(Factory.GetState(nameof(EnemyLookingForTargetState)));
      }
    }
    
    void CheckPath() {
      lastCheckPathTime = Time.time;
      if (!StateMachine.Context.Pathfinder.IsPathPossible(StateMachine.Context.Target.Bounds)) {
        StateMachine.SwitchState(Factory.GetState(nameof(EnemyLookingForTargetState)));
      }
    }
  }
}
