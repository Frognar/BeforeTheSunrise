using System;
using UnityEngine;

namespace bts {
  [CreateAssetMenu(fileName = "Music Channel", menuName = "Channels/Music Channel")]
  public class MusicEventChannel : ScriptableObject {
    public Action<AudioClip> OnMusicPlayRequest;
    public Action OnMusicStopRequest;

    public void RaisePlayEvent(AudioClip clip) {
      OnMusicPlayRequest?.Invoke(clip);
    }

    public void RaiseStopEvent() {
      OnMusicStopRequest?.Invoke();
    }
  }
}
