using fro.States;
using UnityEngine;

namespace bts {
  public class EnemyAttackState : EnemyBaseState {
    float lastAttackTime;
    public bool HasTarget => Context.Target != null && (Context.Target as Object) != null;
    bool IsTimeToAttack => lastAttackTime + Context.EnemyData.TimeBetweenAttacks <= Time.time;
    bool InAttackRange => Vector3.Distance(Context.Position, Context.Target.Position) <= Context.EnemyData.AttackRange;
    float lastCheckPathTime;
    const float checkPathInterval = 2f;
    bool IsTimeToCheckPath => lastCheckPathTime + checkPathInterval <= Time.time;

    public EnemyAttackState(StateMachine<Enemy> stateMachine, StateFactory<Enemy> factory)
      : base(stateMachine, factory) {
    }

    public override void EnterState() {
      if (HasTarget) {
        Context.Pathfinder.SetTarget(Context.Target.Center);
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
      Context.SFXEventChannel.RaisePlayEvent(Context.EnemyData.EnemyAttackSFX, Context.EnemyData.AudioConfig, Context.Position);
      Context.Target.TakeDamage(Context.EnemyData.Damage);
      if (Context.Target.IsDead) {
        Context.Target = null;
        StateMachine.SwitchState(Factory.GetState(nameof(EnemyLookingForTargetState)));
      }
    }
    
    void CheckPath() {
      lastCheckPathTime = Time.time;
      if (!Context.Pathfinder.IsPathPossible(Context.Target.Bounds)) {
        StateMachine.SwitchState(Factory.GetState(nameof(EnemyLookingForTargetState)));
      }
    }
  }
}
