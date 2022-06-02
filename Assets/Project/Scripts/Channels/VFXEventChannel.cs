using System;
using UnityEngine;

namespace bts {
  public abstract class VFXEventChannel<T> : ScriptableObject
    where T : MonoBehaviour, Poolable {
    public Action<VFXConfiguration<T>, VFXParameters<T>> OnVFXEventRequest;
    public Func<VFXConfiguration<T>, VFXParameters<T>, T> OnVFXEventRequestWithReference;

    public void RaiseVFXEvent(VFXConfiguration<T> config, VFXParameters<T> parameters) {
      OnVFXEventRequest?.Invoke(config, parameters);
    }

    public T RaiseVFXEventWithReference(VFXConfiguration<T> config, VFXParameters<T> parameters) {
      return OnVFXEventRequestWithReference?.Invoke(config, parameters);
    }
  }
}
