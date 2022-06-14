using fro.ValueAssets;
using UnityEngine;

namespace bts {
  public class TimeTicker : MonoBehaviour {
    [SerializeField] VoidEventChannel onTick;
    [SerializeField] VoidEventChannel onSecond;
    [SerializeField] IntAsset ticksPerSecond;

    float tickInterval;
    int tick;
    float currentTime;

    void Awake() {
      tickInterval = 1f / ticksPerSecond;
    }

    void Update() {
      currentTime += Time.deltaTime;
      if (currentTime >= tickInterval) {
        tick++;
        currentTime = 0;
        onTick.Invoke();
        if (tick % ticksPerSecond == 0) {
          onSecond.Invoke();
        }
      }
    }
  }
}
