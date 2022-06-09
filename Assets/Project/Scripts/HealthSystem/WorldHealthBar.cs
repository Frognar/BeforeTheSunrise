using UnityEngine;

namespace bts {
  public class WorldHealthBar : MonoBehaviour {
    [SerializeField] Transform background;
    [SerializeField] Transform bar;
    Camera cam;
    public HealthAnimated Health { get; private set; }

    void Awake() {
      cam = Camera.main;
    }

    public void SetUp(Health health) {
      Health = new HealthAnimated(health);
      Health.OnValueChange += OnHealthChange;
      UpdateBar();
    }

    void Update() {
      if (Health != null) {
        Health.Update();
      }
    }

    void LateUpdate() {
      if (cam != null) {
        transform.LookAt(cam.transform);
      }
    }

    void OnDestroy() {
      if (Health != null) {
        Health.OnValueChange -= OnHealthChange;
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
