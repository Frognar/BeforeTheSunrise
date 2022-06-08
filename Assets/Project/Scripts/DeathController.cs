using UnityEditor;
using UnityEngine;

namespace bts {
  public class DeathController : MonoBehaviour {
    [SerializeField] VoidEventChannel deathEventChannel;
    [SerializeField] LoadSceneEventChannel loadSceneEventChannel;

    void OnEnable() {
      deathEventChannel.OnEventInvoked += OnDeath;
    }

    void OnDisable() {
      deathEventChannel.OnEventInvoked -= OnDeath;
    }

    void OnDeath(object sender, System.EventArgs e) {
      loadSceneEventChannel.RaiseOnLoadScene(ScenesNames.DeathScene, unloadCurrent: false);
    }
  }
}
