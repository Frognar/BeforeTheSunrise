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

      List<Damageable> enemiesInRange = InRangeFinder.Find<Damageable>(Context.Position, Context.Range);
      Damageable target = enemiesInRange.FirstOrDefault(t => t.ObjectAffiliation == Affiliation.Enemy && t.IsDead == false);
      if (target != null) {
        Context.Target = target;
        StateMachine.SwitchState(Factory.GetState(nameof(CannonAttackState)));
      }
    }

    public override void ExitState() {
      Context.IsIdle = false;
    }
  }
}
