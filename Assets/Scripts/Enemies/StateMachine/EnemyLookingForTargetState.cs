using UnityEngine;

namespace bts {
  public class EnemyLookingForTargetState : EnemyBaseState {
    public EnemyLookingForTargetState(StateMachine<Enemy> stateMachine, StateFactory<Enemy> factory)
      : base(stateMachine, factory) {
    }
  }
}
