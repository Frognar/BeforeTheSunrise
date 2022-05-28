using UnityEngine;

namespace bts {
  public class MusicController : MonoBehaviour {
    [SerializeField] VoidEventChannel dayStarted;
    [SerializeField] VoidEventChannel nightStarted;
    [SerializeField] MusicEventChannel musicEventChannel;
    [SerializeField] AudioConfiguration config;
    [SerializeField] AudioClip nigthClip;
    [SerializeField] AudioClip dayClip;

    void OnEnable() {
      dayStarted.OnEventInvoked += OnDayStarted;
      nightStarted.OnEventInvoked += OnNightStarted;
    }

    void OnDisable() {
      dayStarted.OnEventInvoked -= OnDayStarted;
      nightStarted.OnEventInvoked -= OnNightStarted;
    }

    void OnDayStarted(object sender, System.EventArgs e) {
      musicEventChannel.RaisePlayEvent(dayClip, config);
    }

    void OnNightStarted(object sender, System.EventArgs e) {
      musicEventChannel.RaisePlayEvent(nigthClip, config);
    }
  }
}