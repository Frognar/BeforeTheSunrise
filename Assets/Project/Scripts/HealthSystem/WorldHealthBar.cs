using System;
using UnityEngine;
using UnityEngine.UI;

namespace fro.HealthSystem {
  public class WorldHealthBar : MonoBehaviour {
    [SerializeField] Canvas healthCanvas;
    [SerializeField] Image healthBar;
    public Health Health { get; private set; }

    public void SetUp(Health health) {
      if (Health != null) {
        Health.OnCurrentHealthChange -= OnHealthChange;
      }

      Health = health;
      Health.OnCurrentHealthChange += OnHealthChange;
      UpdateBar();
    }

    void OnDestroy() {
      if (Health != null) {
        Health.OnCurrentHealthChange -= OnHealthChange;
      }
    }

    void OnHealthChange(object sender, EventArgs eventArgs) {
      UpdateBar();
    }

    void UpdateBar() {
      healthBar.fillAmount = Health.HealthNormalized;
      healthCanvas.enabled = Health.IsInFullHealth == false;
    }

    void LateUpdate() {
      if (Camera.main != null) {
        transform.LookAt(Camera.main.transform);
      }
    }
  }
}
