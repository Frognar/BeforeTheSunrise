using UnityEngine;

namespace bts {
  public class CannonCommander : MonoBehaviour {
    PlayerInputs playerInputs;
    CannonCommandInvoker invoker;
    Cannon cannon;

    void Awake() {
      playerInputs = FindObjectOfType<PlayerInputs>();
      invoker = GetComponent<CannonCommandInvoker>();
      cannon = GetComponent<Cannon>();
    }

    void Update() {
      if (cannon.IsSelected) {
        if (playerInputs.SendCommand) {
          HandleSendingCommands();
        }
      }
    }

    void HandleSendingCommands() {
      if (Physics.Raycast(playerInputs.RayToWorld, out RaycastHit hitInfo)) {
        if (hitInfo.transform.TryGetComponent(out Damageable damageable)
         && (damageable.ObjectAffiliation == Affiliation.Neutral || damageable.ObjectAffiliation == Affiliation.Enemy)) {
          SendCommand(new CannonAttackCommand(cannon, damageable));
        }
        else {
          SendCommand(new CannonStopCommand(cannon));
        }
      }
    }

    void SendCommand(Command command) {
      if (playerInputs.IsCommandQueuingEnabled) {
        invoker.AddCommand(command);
      }
      else {
        invoker.ForceCommandExecution(command);
      }
    }
  }
}
