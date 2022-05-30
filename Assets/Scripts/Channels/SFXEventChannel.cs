using System;
using UnityEngine;

namespace bts {
  [CreateAssetMenu(fileName = "SFX Channel", menuName = "Channels/SFX Channel")]
  public class SFXEventChannel : ScriptableObject {
    public Action<AudioClip, AudioConfiguration, Vector3> OnSFXPlayRequest;
    public Func<AudioClip, AudioConfiguration, Vector3, SoundEmitter> OnSFXPlayRequestWithEmitter;

    public void RaisePlayEvent(AudioClip clip, AudioConfiguration config, Vector3 position = default) {
      OnSFXPlayRequest?.Invoke(clip, config, position);
    }
    
    public SoundEmitter RaisePlayEventWithEmitter(AudioClip clip, AudioConfiguration config, Vector3 position = default) {
      return OnSFXPlayRequestWithEmitter?.Invoke(clip, config, position);
    }
  }
}
