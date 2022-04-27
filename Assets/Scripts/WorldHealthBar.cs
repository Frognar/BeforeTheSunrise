using System;
using UnityEngine;

namespace bts {
  public class WorldHealthBar : MonoBehaviour {
    HealthAnimated Health { get; set; }
    Transform bar;

    public void SetUp(Health health) {
      bar = transform.Find("Hook");
      Health = new HealthAnimated(health, instantDecrease: false);
      Health.OnValueChange += OnHealthChange;
    }

    void OnDestroy() {
      if (Health != null) {
        Health.OnValueChange -= OnHealthChange;
      }
    }

    void OnHealthChange(object sender, EventArgs e) {
      UpdateBar();
    }

    void UpdateBar() {
      if (bar != null) {
        bar.localScale = new Vector3(Health.HealthNormalized, 1f);
      }
    }
  }
}
