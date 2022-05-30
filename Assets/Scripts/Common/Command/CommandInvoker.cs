using System.Collections.Generic;
using UnityEngine;

namespace bts {
  public abstract class CommandInvoker<T> : MonoBehaviour
    where T : MonoBehaviour, CommandReceiver {
    Queue<Command> commands;
    T receiver;

    void Awake() {
      commands = new Queue<Command>();
      receiver = GetComponent<T>();
    }

    public void AddCommand(Command command) {
      commands.Enqueue(command);
    }

    public void ForceCommandExecution(Command command) {
      commands.Clear();
      command.Execute();
    }

    void Update() {
      if (receiver.IsFree()) {
        if (commands.Count > 0) {
          Command command = commands.Dequeue();
          command.Execute();
        }
      }
    }
  }
}
