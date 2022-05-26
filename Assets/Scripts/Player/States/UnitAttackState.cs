using UnityEngine;

namespace bts {
  public class UnitAttackState : UnitBaseState {
    float lastAttackTime;
    public bool HasTarget => StateMachine.Context.Target != null && (StateMachine.Context.Target as Object) != null;
    bool IsTimeToAttack => lastAttackTime + StateMachine.Context.TimeBetweenAttacks <= Time.time;
    bool InAttackRange => Vector3.Distance(StateMachine.Context.Position, StateMachine.Context.Target.Position) <= StateMachine.Context.AttackRange;

    public UnitAttackState(StateMachine<Unit> stateMachine, StateFactory<Unit> factory)
      : base(stateMachine, factory) {
    }

    public override void EnterState() {
      if (HasTarget) {
        StateMachine.Context.Pathfinder.SetTarget(StateMachine.Context.Target.Center);
        StateMachine.Context.Pathfinder.SetStopDistance(StateMachine.Context.AttackRange - 2f);
      }
      else if (!CheckSwitchState()) {
        StateMachine.SwitchState(Factory.GetState(nameof(UnitIdleState)));
      }
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

    protected override bool CheckSwitchState() {
      if (base.CheckSwitchState()) {
        return true;
      }

      if (!HasTarget) {
        StateMachine.SwitchState(Factory.GetState(nameof(UnitIdleState)));
        return true;
      }

      return false;
    }

    void Attack() {
      lastAttackTime = Time.time;
      StateMachine.Context.Target.TakeDamage(StateMachine.Context.DamageAmount);
      if (StateMachine.Context.Target.IsDead) {
        StateMachine.Context.Target = null;
        StateMachine.SwitchState(Factory.GetState(nameof(UnitIdleState)));
      }
    }

    public override void ExitState() {
      StateMachine.Context.Pathfinder.Reset();
    }
  }
}