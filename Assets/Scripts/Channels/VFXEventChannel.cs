using System;
using UnityEngine;

namespace bts {
  [CreateAssetMenu(fileName = "VFX Channel", menuName = "Channels/VFX Channel")]
  public class VFXEventChannel : ScriptableObject {
    public Action<Transform, Transform, Vector3, float> OnVFXEventRequest;

    public void RaiseVFXEvent(Transform source, Transform target, Vector3 color, float duration) {
      OnVFXEventRequest?.Invoke(source, target, color, duration);
    }
  }
}
