using System;
using UnityEngine;

namespace bts {
  public class TimeTicker : MonoBehaviour {
    public static EventHandler OnSecond;

    const int ticksPerSeconds = 5;
    const float tickInterval = 1f / ticksPerSeconds;

    int tick;
    float currentTime;

    void Update() {
      currentTime += Time.deltaTime;
      if (currentTime >= tickInterval) {
        tick++;
        currentTime = 0;
        if (tick % ticksPerSeconds == 0) {
          OnSecond?.Invoke(this, EventArgs.Empty);
        }
      }
    }
  }
}
