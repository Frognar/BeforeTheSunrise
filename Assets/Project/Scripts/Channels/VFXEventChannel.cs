using System;
using UnityEngine;

namespace bts {
  [CreateAssetMenu(fileName = "VFX Channel", menuName = "Channels/VFX Channel")]
  public class VFXEventChannel : ScriptableObject {
    public Action<Transform, Vector3, ElectricArcVFXConfiguration> OnVFXEventRequest;

    public void RaiseVFXEvent(Transform source, Vector3 target, ElectricArcVFXConfiguration config) {
      OnVFXEventRequest?.Invoke(source, target, config);
    }
  }
}
