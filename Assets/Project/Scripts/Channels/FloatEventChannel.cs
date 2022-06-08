using System;
using UnityEngine;

namespace bts {
  [CreateAssetMenu(fileName = "Float Channel", menuName = "Channels/Float Channel")]
  public class FloatEventChannel : ScriptableObject {
    public Action<float> OnEventRaised = delegate { };

    public void RaiseEvent(float value) {
      OnEventRaised.Invoke(value);
    }
  }
}
