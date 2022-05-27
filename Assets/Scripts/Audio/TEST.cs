using System;
using UnityEngine;

namespace bts {
  public class TEST : MonoBehaviour {
    [SerializeField] VoidEventChannel onSecond;
    [SerializeField] AudioClip clip;
    [SerializeField] SFXEventChannel sfxEventChannel;

    void Start() {
      onSecond.OnEventInvoked += Play;
    }

    void OnDestroy() {
      onSecond.OnEventInvoked -= Play;
    }

    void Play(object sender, EventArgs e) {
      sfxEventChannel.RaisePlayEvent(clip);
    }
  }
}
