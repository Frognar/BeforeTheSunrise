using System.Collections.Generic;
using UnityEngine;

namespace bts {
  public class HealerCommandInvoker : MonoBehaviour {
    Queue<Command> commands;
    Healer healer;

    void Awake() {
      commands = new Queue<Command>();
      healer = GetComponent<Healer>();
    }

    public void AddCommand(Command command) {
      commands.Enqueue(command);
    }

    public void ForceCommandExecution(Command command) {
      commands.Clear();
      command.Execute();
    }

    void Update() {
      if (healer.IsIdle) {
        if (commands.Count > 0) {
          commands.Dequeue().Execute();
        }
      }
    }
  }
}
