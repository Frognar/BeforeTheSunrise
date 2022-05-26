using UnityEngine;

namespace bts {
  public class EnemyLookingForTargetState : EnemyBaseState {
    public EnemyLookingForTargetState(StateMachine<Enemy> stateMachine, StateFactory<Enemy> factory)
      : base(stateMachine, factory) {
    }

    public override void EnterState() {
      StateMachine.Context.Pathfinder.Reset();
      Damageable player = Object.FindObjectOfType<Unit>();
      if (!SetAsTargetIfPathExitst(player)) {
        Building[] damageables = Object.FindObjectsOfType<Building>();
        foreach (Damageable damageable in damageables) {
          if (SetAsTargetIfPathExitst(damageable)) {
            break;
          }
        }
      }

      if (StateMachine.Context.Target == null) {
        Debug.Log("No Target");
      }
    }

    bool SetAsTargetIfPathExitst(Damageable possibleTarget) {
      if (possibleTarget as Object is null) {
        return false;
      }
      
      if (StateMachine.Context.Pathfinder.IsPathPossible(possibleTarget.Bounds)) {
        StateMachine.Context.Target = possibleTarget;
        StateMachine.SwitchState(Factory.GetState(nameof(EnemyAttackState)));
        return true;
      }

      return false;
    }
  }
}
