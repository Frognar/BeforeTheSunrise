using UnityEngine;

namespace bts {
  public class HealerCommander : Commander<Healer> {
    protected override void HandleSendingCommands(Ray rayToWorld) {
      if (receiver.IsSelected) {
        if (Physics.Raycast(rayToWorld, out RaycastHit hitInfo)) {
          if (hitInfo.transform.TryGetComponent(out Damageable damageable)
           && (damageable.ObjectAffiliation == Affiliation.Neutral || damageable.ObjectAffiliation == Affiliation.Player)) {
            SendCommand(new HealerHealCommand(receiver, damageable));
          }
          else {
            SendCommand(new HealerStopCommand(receiver));
          }
        }
      }
    }
  }
}
