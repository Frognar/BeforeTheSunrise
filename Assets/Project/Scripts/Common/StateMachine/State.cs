﻿namespace fro.States {
  public abstract class State<T> {
    protected StateMachine<T> StateMachine { get; }
    protected StateFactory<T> Factory { get; }
    protected T Context => StateMachine.Context;

    protected State(StateMachine<T> stateMachine, StateFactory<T> factory) {
      StateMachine = stateMachine;
      Factory = factory;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
  }
}
