using UnityEngine;

namespace bts {
  public class CannonCommander : MonoBehaviour {
    [SerializeField] InputReader inputReader;
    CannonCommandInvoker invoker;
    Cannon cannon;

    void Awake() {
      invoker = GetComponent<CannonCommandInvoker>();
      cannon = GetComponent<Cannon>();
    }

    void OnEnable() {
      inputReader.SendCommandEvent += HandleSendingCommands;
    }

    void OnDisable() {
      inputReader.SendCommandEvent -= HandleSendingCommands;
    }

    void HandleSendingCommands(Ray rayToWorld) {
      if (cannon.IsSelected) {
        if (Physics.Raycast(rayToWorld, out RaycastHit hitInfo)) {
          if (hitInfo.transform.TryGetComponent(out Damageable damageable)
           && (damageable.ObjectAffiliation == Affiliation.Neutral || damageable.ObjectAffiliation == Affiliation.Enemy)) {
            SendCommand(new CannonAttackCommand(cannon, damageable));
          }
          else {
            SendCommand(new CannonStopCommand(cannon));
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
