using System;
using UnityEngine;

namespace bts {
  public class TimeTicker : MonoBehaviour {
    public static EventHandler OnTick;
    public static EventHandler OnSecond;

    public const int ticksPerSecond = 5;
    const float tickInterval = 1f / ticksPerSecond;

    int tick;
    float currentTime;

    void Update() {
      currentTime += Time.deltaTime;
      if (currentTime >= tickInterval) {
        tick++;
        currentTime = 0;
        OnTick?.Invoke(this, EventArgs.Empty);
        if (tick % ticksPerSecond == 0) {
          OnSecond?.Invoke(this, EventArgs.Empty);
        }
      }
    }
  }
}
