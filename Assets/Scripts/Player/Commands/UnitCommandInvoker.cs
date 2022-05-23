using System.Collections.Generic;
using UnityEngine;

namespace bts {
  public class UnitCommandInvoker : MonoBehaviour {
    Queue<Command> commands;
    Unit unit;

    void Awake() {
      commands = new Queue<Command>();
      unit = GetComponent<Unit>();
    }

    public void AddCommand(Command command) {
      commands.Enqueue(command);
    }

    public void ForceCommandExecution(Command command) {
      commands.Clear();
      command.Execute();
    }

    void Update() {
      if (unit.IsIdle || unit.IsGathering) {
        if (commands.Count > 0) {
          commands.Dequeue().Execute();
        }
      }
    }
  }
}
