using System;
using UnityEngine;

namespace bts {
  public class WorldHealthBar : MonoBehaviour {
    [SerializeField] VoidEventChannel onTick;
    [SerializeField] Transform background;
    [SerializeField] Transform bar;
    HealthAnimated Health { get; set; }

    public void SetUp(Health health) {
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
      bar.localScale = new Vector3(Health.HealthNormalized, 1f);
      background.gameObject.SetActive(!Health.HasFullHealth);
      bar.gameObject.SetActive(!Health.HasFullHealth);
    }
  }
}
