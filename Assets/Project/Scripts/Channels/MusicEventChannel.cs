using System;
using UnityEngine;

namespace bts {
  [CreateAssetMenu(fileName = "Music Channel", menuName = "Channels/Music Channel")]
  public class MusicEventChannel : ScriptableObject {
    public Action<AudioClip, AudioConfiguration> OnMusicPlayRequest;
    public Action OnMusicStopRequest;

    public void RaisePlayEvent(AudioClip clip, AudioConfiguration config) {
      OnMusicPlayRequest?.Invoke(clip, config);
    }

    public void RaiseStopEvent() {
      OnMusicStopRequest?.Invoke();
    }
  }
}
