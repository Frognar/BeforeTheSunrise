using System.Collections.Generic;
using UnityEngine;

namespace bts {
  public abstract class StateFactory<T> {
    protected StateMachine<T> StateMachine { get; }
    
    protected StateFactory(StateMachine<T> stateMachine) {
      StateMachine = stateMachine;
      States = CreateStates();
    }

    protected abstract Dictionary<string, State<T>> CreateStates();

    protected Dictionary<string, State<T>> States { get; }
    public State<T> GetState(string name) {
      if (States.ContainsKey(name)) {
        return States[name];
      }
      else {
        Debug.LogWarning($"State {name} not found");
        return null;
      }
    }
  }
}
