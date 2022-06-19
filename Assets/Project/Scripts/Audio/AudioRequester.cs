using UnityEngine;

namespace bts {
  public class AudioRequester : MonoBehaviour {
    [SerializeField] SFXEventChannel sfxEventChannel;
    [SerializeField] AudioConfiguration audioConfiguration;
    SoundEmitter soundEmitter;
    
    public void StartSFX(AudioClipsGroup sfx, Vector3 position) {
      if (soundEmitter == null) {
        soundEmitter = sfxEventChannel.RaisePlayEventWithEmitter(sfx, audioConfiguration, position);
      }
    }

    public void StopSFX() {
      if (soundEmitter != null) {
        soundEmitter.Stop();
        soundEmitter = null;
      }
    }

    public void RequestSFX(AudioClipsGroup sfx, Vector3 position) {
      sfxEventChannel.RaisePlayEvent(sfx, audioConfiguration, position);
    }

    void OnDestroy() {
      StopSFX();
    }
  }
}
