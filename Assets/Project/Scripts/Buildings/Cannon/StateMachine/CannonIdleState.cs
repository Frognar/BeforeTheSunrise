using System;
using System.Collections.Generic;
using System.Linq;
using fro.States;

namespace bts {
  public class CannonIdleState : CannonBaseState {
    public CannonIdleState(StateMachine<Cannon> stateMachine, StateFactory<Cannon> factory)
      : base(stateMachine, factory) {
    }
    
    public override void EnterState() {
      Context.IsIdle = true;
    }

    public override void UpdateState() {
      if (CheckSwitchState()) {
        return;
      }

      List<Damageable> enemiesInRange = InRangeFinder.Find(Context.Position, Context.Range, customPredicate: livingEnemy);
      if (enemiesInRange.Count > 0) {
        Context.Target = enemiesInRange[0];
        StateMachine.SwitchState(Factory.GetState(nameof(CannonAttackState)));
      }
    }

    readonly Func<Damageable, bool> livingEnemy = t => t.ObjectAffiliation == Affiliation.Enemy && t.IsDead == false;

    public override void ExitState() {
      Context.IsIdle = false;
    }
  }
}
