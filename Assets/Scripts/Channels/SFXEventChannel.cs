using System;
using UnityEngine;

namespace bts {
  [CreateAssetMenu(fileName = "SFX Channel", menuName = "Channels/SFX Channel")]
  public class SFXEventChannel : ScriptableObject {
    public Action<AudioClip, AudioConfiguration, Vector3> OnSFXPlayRequest;

    public void RaisePlayEvent(AudioClip clip, AudioConfiguration config, Vector3 position = default) {
      OnSFXPlayRequest?.Invoke(clip, config, position);
    }
  }
}
