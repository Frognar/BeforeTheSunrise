using UnityEngine;
using UnityEngine.InputSystem;

namespace bts {
  [RequireComponent(typeof(PlayerInput))]
  public class Selector : MonoBehaviour {
    Camera cam;
    Vector2 screenPosition;
    Transform selected;

    void Awake() {
      cam = Camera.main; 
    }

    public void OnScreenPosition(InputValue value) {
      screenPosition = value.Get<Vector2>();
    }

    public void OnLeftClick() {
      selected = null;
      Ray ray = cam.ScreenPointToRay(screenPosition);
      if (Physics.Raycast(ray, out RaycastHit hitInfo)) {
        if (hitInfo.transform.TryGetComponent(out Selectable selectable)) {
          selectable.Select();
          selected = hitInfo.transform;
        }
      }
    }

    public void OnRightClick() {
      if (selected != null) {
        if (selected.TryGetComponent(out Unit unit)) {
          Ray ray = cam.ScreenPointToRay(screenPosition);
          unit.Execute(ray);
        }
      }
    }
  }
}
