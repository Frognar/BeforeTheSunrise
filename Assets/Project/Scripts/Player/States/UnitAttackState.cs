using fro.States;
using UnityEngine;

namespace bts {
  public class UnitAttackState : UnitBaseState {
    float lastAttackTime;
    public bool HasTarget => Context.Target != null && (Context.Target as Object) != null;
    bool IsTimeToAttack => lastAttackTime + Context.TimeBetweenAttacks <= Time.time;
    bool InAttackRange => Vector3.Distance(Context.Position, Context.Target.Position) <= Context.AttackRange;

    public UnitAttackState(StateMachine<Unit> stateMachine, StateFactory<Unit> factory)
      : base(stateMachine, factory) {
    }

    public override void EnterState() {
      if (HasTarget) {
        Context.Pathfinder.SetTarget(Context.Target.Center);
        Context.Pathfinder.SetStopDistance(Context.AttackRange - 2f);
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
      Context.ElectricArcRequester.Create(Context.Target.Center.position);
      Context.AudioRequester.RequestSFX(Context.AttackSFX, Context.Position);
      Context.Target.TakeDamage(Context.DamageAmount);
      if (Context.Target.IsDead) {
        Context.Target = null;
        StateMachine.SwitchState(Factory.GetState(nameof(UnitIdleState)));
      }
    }

    public override void ExitState() {
      Context.Pathfinder.Reset();
    }
  }
}