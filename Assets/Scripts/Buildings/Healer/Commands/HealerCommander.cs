using UnityEngine;

namespace bts {
  public class HealerCommander : MonoBehaviour {
    [SerializeField] InputReader inputReader;
    HealerCommandInvoker invoker;
    Healer healer;

    void Awake() {
      invoker = GetComponent<HealerCommandInvoker>();
      healer = GetComponent<Healer>();
    }

    void OnEnable() {
      inputReader.SendCommandEvent += HandleSendingCommands;
    }

    void OnDisable() {
      inputReader.SendCommandEvent -= HandleSendingCommands;
    }

    void HandleSendingCommands(Ray rayToWorld) {
      if (healer.IsSelected) {
        if (Physics.Raycast(rayToWorld, out RaycastHit hitInfo)) {
          if (hitInfo.transform.TryGetComponent(out Damageable damageable)
           && (damageable.ObjectAffiliation == Affiliation.Neutral || damageable.ObjectAffiliation == Affiliation.Player)) {
            SendCommand(new HealerHealCommand(healer, damageable));
          }
          else {
            SendCommand(new HealerStopCommand(healer));
          }
        }
      }
    }

    void SendCommand(Command command) {
      if (inputReader.IsCommandQueuingEnabled) {
        invoker.AddCommand(command);
      }
      else {
        invoker.ForceCommandExecution(command);
      }
    }
  }
}
