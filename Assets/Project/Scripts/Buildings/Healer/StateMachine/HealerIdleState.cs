using System;
using System.Collections.Generic;
using System.Linq;
using fro.States;
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

      
      List<Healable> healableInRange = InRangeFinder.Find(Context.Position, Context.Range, customPredicate: injuredAlly);
      if (healableInRange.Count > 0) {
        Context.Target = healableInRange.First();
        StateMachine.SwitchState(Factory.GetState(nameof(HealerHealState)));
      }
    }

    readonly Func<Healable, bool> injuredAlly = t => t.ObjectAffiliation == Affiliation.Player && t.IsIntact == false;

    public override void ExitState() {
      Context.IsIdle = false;
    }
  }
}
