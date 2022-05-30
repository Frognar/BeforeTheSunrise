using UnityEngine;

namespace bts {
  public abstract class Commander<T> : MonoBehaviour
    where T : MonoBehaviour, CommandReceiver {
    [SerializeField] protected InputReader inputReader;
    CommandInvoker<T> invoker;
    protected T receiver;

    protected virtual void Awake() {
      invoker = GetComponent<CommandInvoker<T>>();
      receiver = GetComponent<T>();
    }

    protected virtual void OnEnable() {
      inputReader.SendCommandEvent += HandleSendingCommands;
    }

    protected virtual void OnDisable() {
      inputReader.SendCommandEvent -= HandleSendingCommands;
    }

    protected abstract void HandleSendingCommands(Ray rayToWorld);
    
    protected virtual void SendCommand(Command command) {
      if (inputReader.IsCommandQueuingEnabled) {
        invoker.AddCommand(command);
      }
      else {
        invoker.ForceCommandExecution(command);
      }
    }
  }
}
