using System;
using UnityEngine;

namespace bts {
  [CreateAssetMenu(fileName = "SFX Channel", menuName = "Channels/SFX Channel")]
  public class SFXEventChannel : ScriptableObject {
    public Action<AudioClip, Vector3> OnSFXPlayRequest;

    public void RaisePlayEvent(AudioClip clip, Vector3 position = default) {
      OnSFXPlayRequest?.Invoke(clip, position);
    }
  }
}
