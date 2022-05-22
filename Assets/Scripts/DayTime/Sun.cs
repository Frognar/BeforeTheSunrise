using System;
using System.Collections;
using UnityEngine;

namespace bts {
  public class Sun : MonoBehaviour {
    [SerializeField] VoidEventChannel dayStarted;
    [SerializeField] VoidEventChannel nightStarted;
    [SerializeField] Light sun;
    [SerializeField] float dayLightIntensity = 2f;
    [SerializeField] int dayLightTemperature = 5000;
    [SerializeField] float nightLightIntensity = 0.5f;
    [SerializeField] int nightLightTemperature = 20000;
    [SerializeField] float fadeDuration = 2f;

    void OnEnable() {
      dayStarted.OnEventInvoked += ChangeToDay;
      nightStarted.OnEventInvoked += ChangeToNight;
    }

    void OnDisable() {
      dayStarted.OnEventInvoked -= ChangeToDay;
      nightStarted.OnEventInvoked -= ChangeToNight;
    }

    void ChangeToDay(object sender, EventArgs e) {
      _ = StartCoroutine(FadeSun(nightLightIntensity, dayLightIntensity, nightLightTemperature, dayLightTemperature));
    }

    void ChangeToNight(object sender, EventArgs e) {
      _ = StartCoroutine(FadeSun(dayLightIntensity, nightLightIntensity, dayLightTemperature, nightLightTemperature));
    }

    IEnumerator FadeSun(float startIntensity, float endIntensity, int startTemperature, int endTemperature) {
      float elapsed = 0f;
      while (elapsed < fadeDuration) {
        float t = elapsed / fadeDuration;
        sun.intensity = Mathf.Lerp(startIntensity, endIntensity, t);
        sun.colorTemperature = Mathf.Lerp(startTemperature, endTemperature, t);
        elapsed += Time.deltaTime;
        yield return null;
      }
      
      sun.intensity = endIntensity;
      sun.colorTemperature = endTemperature;
    }
  }
}
