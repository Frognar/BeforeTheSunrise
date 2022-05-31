using System;
using UnityEngine;

namespace bts {
  [CreateAssetMenu(fileName = "Blood Channel", menuName = "Channels/Blood Channel")]
  public class BloodEventChannel : ScriptableObject {
    public Func<Vector3, BloodConfiguration, Blood> OnBloodEventRequest;

    public Blood RaiseBloodEvent(Vector3 source, BloodConfiguration config) {
      return OnBloodEventRequest?.Invoke(source, config);
    }
  }
}
