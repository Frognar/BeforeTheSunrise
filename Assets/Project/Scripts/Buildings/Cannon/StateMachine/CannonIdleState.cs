using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace bts {
  public class CannonIdleState : CannonBaseState {
    public CannonIdleState(StateMachine<Cannon> stateMachine, StateFactory<Cannon> factory)
      : base(stateMachine, factory) {
    }
    
    public override void EnterState() {
      StateMachine.Context.IsIdle = true;
    }

    public override void UpdateState() {
      if (CheckSwitchState()) {
        return;
      }

      Collider[] collidersInRange = Physics.OverlapSphere(StateMachine.Context.Position, StateMachine.Context.Range);
      List<Damageable> enemiesInRange = new List<Damageable>();
      foreach (Collider collider in collidersInRange) {
        if (collider.TryGetComponent(out Damageable damageable) && damageable.ObjectAffiliation == Affiliation.Enemy) {
          enemiesInRange.Add(damageable);
        }
      }

      Damageable target = enemiesInRange.FirstOrDefault(t => !t.IsDead);
      if (target != null) {
        StateMachine.Context.Target = target;
        StateMachine.SwitchState(Factory.GetState(nameof(CannonAttackState)));
      }
    }

    public override void ExitState() {
      StateMachine.Context.IsIdle = false;
    }
  }
}
