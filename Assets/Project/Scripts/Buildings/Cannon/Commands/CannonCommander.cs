using UnityEngine;

namespace bts {
  public class CannonCommander : Commander<Cannon> {
    protected override void HandleSendingCommands(Ray rayToWorld) {
      if (receiver.IsSelected) {
        if (Physics.Raycast(rayToWorld, out RaycastHit hitInfo)) {
          if (hitInfo.transform.TryGetComponent(out Damageable damageable)
           && (damageable.ObjectAffiliation == Affiliation.Neutral || damageable.ObjectAffiliation == Affiliation.Enemy)) {
            SendCommand(new CannonAttackCommand(receiver, damageable));
          }
          else {
            SendCommand(new CannonStopCommand(receiver));
          }
        }
      }
    }
  }
}
