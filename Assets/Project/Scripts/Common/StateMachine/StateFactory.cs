using System.Collections.Generic;
using UnityEngine;

namespace fro.States {
  public abstract class StateFactory<T> {
    protected StateMachine<T> StateMachine { get; }
    protected Dictionary<string, State<T>> States { get; }

    protected StateFactory(StateMachine<T> stateMachine) {
      StateMachine = stateMachine;
      States = CreateStates();
    }

    protected abstract Dictionary<string, State<T>> CreateStates();

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
