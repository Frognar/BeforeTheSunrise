using System;
using UnityEngine;

namespace bts {
  public class WorldHealthBar : MonoBehaviour {
    [SerializeField] VoidEventChannel onTick;
    HealthAnimated Health { get; set; }
    Transform bar;

    public void SetUp(Health health) {
      bar = transform.Find("Hook");
      Health = new HealthAnimated(health);
      Health.OnValueChange += OnHealthChange;
      onTick.OnEventInvoked += Health.UpdateOnTick;
      UpdateBar();
    }

    void LateUpdate() {
      transform.LookAt(Camera.main.transform);
    }

    void OnDestroy() {
      if (Health != null) {
        Health.OnValueChange -= OnHealthChange;
        onTick.OnEventInvoked -= Health.UpdateOnTick;
      }
    }

    void OnHealthChange() {
      UpdateBar();
    }

    void UpdateBar() {
      if (bar != null) {
        bar.localScale = new Vector3(Health.HealthNormalized, 1f);
      }
    }
  }
}
