namespace bts {
  public abstract class StateMachine<T> {
    public T Context { get; }
    protected State<T> currentState;

    protected StateMachine(T context) {
      Context = context;
    }

    public void SwitchState(State<T> newState) {
      if (currentState != null) {
        currentState.ExitState();
      }
      
      currentState = newState;
      currentState.EnterState();
    }

    public abstract void Start();

    public void Update() {
      if (currentState != null) {
        currentState.UpdateState();
      }
    }
  }
}
