using System;
using UnityEngine;

namespace bts {
  [CreateAssetMenu(fileName = "Laser Channel", menuName = "Channels/Laser Channel")]
  public class LaserEventChannel : ScriptableObject {
    public Func<Vector3, Transform, LaserConfiguration, Laser> OnLaserEventRequest;

    public Laser RaiseLaserEvent(Vector3 source, Transform target, LaserConfiguration config) {
      return OnLaserEventRequest?.Invoke(source, target, config);
    }
  }
}
