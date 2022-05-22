using System.Collections.Generic;
using UnityEngine;

namespace bts {
  public class CannonCommandInvoker : MonoBehaviour {
    Queue<Command> commands;
    Cannon cannon;

    void Awake() {
      commands = new Queue<Command>();
      cannon = GetComponent<Cannon>();
    }

    public void AddCommand(Command command) {
      commands.Enqueue(command);
    }

    public void ForceCommandExecution(Command command) {
      commands.Clear();
      command.Execute();
    }

    void Update() {
      if (cannon.IsIdle) {
        if (commands.Count > 0) {
          commands.Dequeue().Execute();
        }
      }  
    }
  }
}
