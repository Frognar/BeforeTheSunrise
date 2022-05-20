using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace bts {
  public class CannonIdleState : CannonBaseState {
    public CannonIdleState(Cannon context, CannonStateFactory factory)
      : base(context, factory) {
    }

    public override void EnterState() {
      Context.IsIdle = true;
    }

    public override void UpdateState() {
      if (CheckSwitchState()) {
        return;
      }

      Collider[] collidersInRange = Physics.OverlapSphere(Context.Position, Context.Range);
      List<Damageable> enemiesInRange = new List<Damageable>();
      foreach (Collider collider in collidersInRange) {
        if (collider.TryGetComponent(out Damageable damageable) && damageable.ObjectAffiliation == Affiliation.Enemy) {
          enemiesInRange.Add(damageable);
        }
      }

      Damageable target = enemiesInRange.FirstOrDefault(t => !t.IsDead);
      if (target != null) {
        Context.Target = target;
        SwitchState(Factory.Attack);
      }
    }

    public override void ExitState() {
      Context.IsIdle = false;
    }
  }
}
