using UnityEngine;
using UnityEngine.InputSystem;

namespace bts {
  [RequireComponent(typeof(PlayerInput))]
  public class Player : MonoBehaviour {
    Camera cam;
    Vector2 screenPosition;
    Selectable selected;

    void Awake() {
      cam = Camera.main; 
    }

    public void OnScreenPosition(InputValue value) {
      screenPosition = value.Get<Vector2>();
    }

    public void OnLeftClick() {
      if (selected != null && (selected as Object) != null) { 
        selected.Deselect();
        selected = null;
      }

      Ray ray = cam.ScreenPointToRay(screenPosition);
      if (Physics.Raycast(ray, out RaycastHit hitInfo)) {
        if (hitInfo.transform.TryGetComponent(out Selectable selectable)) {
          selectable.Select();
          selected = selectable;
        }
      }
    }

    public void OnRightClick() {
      if (selected != null) {
        if (selected.Transform.TryGetComponent(out Unit unit)) {
          Ray ray = cam.ScreenPointToRay(screenPosition);
          unit.Execute(ray);
        }
      }
    }
  }
}
