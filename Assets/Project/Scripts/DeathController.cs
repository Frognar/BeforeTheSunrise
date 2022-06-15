using UnityEngine;

namespace bts {
  public class DeathController : MonoBehaviour {
    [SerializeField] VoidEventChannel deathEventChannel;
    [SerializeField] DeathPanel deathPanel;

    void OnEnable() {
      deathEventChannel.OnEventInvoked += OnDeath;
    }

    void OnDisable() {
      deathEventChannel.OnEventInvoked -= OnDeath;
    }

    void OnDeath(object sender, System.EventArgs e) {
      deathPanel.Show();
    }
  }
}
