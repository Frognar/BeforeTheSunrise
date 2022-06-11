using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace bts {
  public class HealerIdleState : HealerBaseState {
    public HealerIdleState(StateMachine<Healer> stateMachine, StateFactory<Healer> factory)
      : base(stateMachine, factory) {
    }

    public override void EnterState() {
      Context.IsIdle = true;
    }

    public override void UpdateState() {
      if (CheckSwitchState()) {
        return;
      }

      Collider[] collidersInRange = Physics.OverlapSphere(Context.Position, Context.Range);
      List<Damageable> buildingsInRange = new List<Damageable>();
      foreach (Collider collider in collidersInRange) {
        if (collider.TryGetComponent(out Damageable damageable) && damageable.ObjectAffiliation == Affiliation.Player) {
          buildingsInRange.Add(damageable);
        }
      }

      Damageable target = buildingsInRange.FirstOrDefault(t => !t.IsDead && !t.IsIntact);
      if (target != null) {
        Context.Target = target;
        StateMachine.SwitchState(Factory.GetState(nameof(HealerHealState)));
      }
    }

    public override void ExitState() {
      Context.IsIdle = false;
    }
  }
}
