using UnityEngine;

namespace bts {
  public class HealerCommander : Commander<Healer> {
    protected override void HandleSendingCommands(Ray rayToWorld) {
      if (receiver.IsSelected) {
        if (Physics.Raycast(rayToWorld, out RaycastHit hitInfo)) {
          if (hitInfo.transform.TryGetComponent(out Healable healable)
           && (healable.ObjectAffiliation == Affiliation.Neutral || healable.ObjectAffiliation == Affiliation.Player)) {
            SendCommand(new HealerHealCommand(receiver, healable));
          }
          else {
            SendCommand(new HealerStopCommand(receiver));
          }
        }
      }
    }
  }
}
