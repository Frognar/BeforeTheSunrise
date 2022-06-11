using System.Collections;
using fro.States;
using UnityEngine;

namespace bts {
  public class EnemyLookingForTargetState : EnemyBaseState {
    public EnemyLookingForTargetState(StateMachine<Enemy> stateMachine, StateFactory<Enemy> factory)
      : base(stateMachine, factory) {
    }

    public override void EnterState() {
      Context.Pathfinder.Reset();
      Damageable player = Object.FindObjectOfType<Unit>();
      if (!SetAsTargetIfPathExitst(player)) {
        Building[] damageables = Object.FindObjectsOfType<Building>();
        foreach (Damageable damageable in damageables) {
          if (SetAsTargetIfPathExitst(damageable)) {
            break;
          }
        }
      }

      if (Context.Target == null) {
        _ = Context.StartCoroutine(RestartAfterWhile());
      }
    }

    bool SetAsTargetIfPathExitst(Damageable possibleTarget) {
      if (possibleTarget as Object is null) {
        return false;
      }
      
      if (Context.Pathfinder.IsPathPossible(possibleTarget.Bounds)) {
        Context.Target = possibleTarget;
        StateMachine.SwitchState(Factory.GetState(nameof(EnemyAttackState)));
        return true;
      }

      return false;
    }
  
    IEnumerator RestartAfterWhile() { 
      yield return new WaitForSeconds(2f);
      StateMachine.SwitchState(Factory.GetState(nameof(EnemyLookingForTargetState)));
    }
  }
}
