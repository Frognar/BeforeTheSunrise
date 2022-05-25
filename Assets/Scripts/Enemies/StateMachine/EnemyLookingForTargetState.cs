using System.Collections.Generic;
using System.Linq;
using Pathfinding;
using UnityEngine;

namespace bts {
  public class EnemyLookingForTargetState : EnemyBaseState {
    public EnemyLookingForTargetState(StateMachine<Enemy> stateMachine, StateFactory<Enemy> factory)
      : base(stateMachine, factory) {
    }

    public override void EnterState() {
      Damageable player = Object.FindObjectOfType<Unit>();
      if (IsPathPossible(player)) {
        StateMachine.Context.Target = player;
        StateMachine.SwitchState(Factory.GetState(nameof(EnemyAttackState)));
      }
      else {
        Building[] damageables = Object.FindObjectsOfType<Building>();
        foreach (Damageable damageable in damageables) {
          if (IsPathPossible(damageable)) {
            StateMachine.Context.Target = damageable;
            StateMachine.SwitchState(Factory.GetState(nameof(EnemyAttackState)));
            break;
          }
        }
      }

      if (StateMachine.Context.Target == null) {
        Debug.Log("No Target");
      }
    }

    bool IsPathPossible(Damageable target) {
      if (target as Object is null) {
        return false;
      }
      
      GraphNode myNode = (AstarPath.active.graphs[0] as GridGraph).GetNearest(StateMachine.Context.Position).node;
      Bounds bounds = new Bounds(target.Bounds.center, target.Bounds.size + Vector3.one * 2);
      List<GraphNode> nodes = (AstarPath.active.graphs[0] as GridGraph).GetNodesInRegion(bounds);
      return nodes.Any(targetNode => PathUtilities.IsPathPossible(myNode, targetNode));
    }
  }
}
