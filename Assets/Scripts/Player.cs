using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace bts {
  public class Player : MonoBehaviour, PlayerControls.ICursorActions {
    Camera cam;
    Vector2 screenPosition;
    Vector3 startPosition;
    bool isSelecting;
    List<Selectable> selected;
    PlayerControls inputControls;
    LineRenderer lineRenderer;

    void Awake() {
      lineRenderer = GetComponent<LineRenderer>();
      selected = new List<Selectable>();
      cam = Camera.main;
      inputControls = new PlayerControls();
      inputControls.Cursor.SetCallbacks(this);
      lineRenderer.enabled = false;
    }

    void OnEnable() {
      inputControls.Enable();
    }

    void OnDisable() {
      inputControls.Disable();
    }

    public void OnScreenPosition(InputAction.CallbackContext context) {
      screenPosition = context.ReadValue<Vector2>();
    }

    void Update() {
      if (isSelecting) {
        Ray ray = cam.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo)) {
          Vector3 endPosition = hitInfo.point;
          Vector3 lowerLeft = new Vector3(
            Mathf.Min(startPosition.x, endPosition.x),
            1f,
            Mathf.Min(startPosition.z, endPosition.z));
          Vector3 upperRight = new Vector3(
            Mathf.Max(startPosition.x, endPosition.x),
            1f,
            Mathf.Max(startPosition.z, endPosition.z));
          lineRenderer.SetPositions(
            new Vector3[] {
              lowerLeft,
              new Vector3(lowerLeft.x, 1f, upperRight.z),
              upperRight,
              new Vector3(upperRight.x, 1f, lowerLeft.z),
            }
            );
        }
      }
    }

    public void OnLeftClick(InputAction.CallbackContext context) {
      if (context.started) {
        isSelecting = true;
        Ray ray = cam.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo)) {
          startPosition = hitInfo.point;
        }
      }
      
      if (context.canceled) {
        isSelecting = false;
        foreach (Selectable selectable in selected) {
          selectable.Deselect();
        }
        
        selected.Clear();
        Ray ray = cam.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo)) {
          Vector3 endPosition = hitInfo.point;
          Vector3 center = (startPosition + endPosition) / 2f;
          Vector3 halfExtents = new Vector3(Mathf.Abs(startPosition.x - endPosition.x)/2, 2, Mathf.Abs(startPosition.z - endPosition.z)/2);
          Collider[] objects = Physics.OverlapBox(center, halfExtents);
          foreach (Collider obj in objects) {
            if (obj.TryGetComponent(out Selectable selectable)) {
              selected.Add(selectable);
            }
          }

          List<Selectable> selectedUnit = selected.FindAll(s => s.Transform.TryGetComponent(out Unit _));
          if (selectedUnit.Any()) {
            selected = selectedUnit;
          }
        }

        foreach (Selectable selectable in selected) {
          selectable.Select();
        }
      }

      lineRenderer.enabled = isSelecting;
    }

    public void OnRightClick(InputAction.CallbackContext context) {
      foreach (Selectable selectable in selected) {
        if (selectable.Transform.TryGetComponent(out Unit unit)) {
          Ray ray = cam.ScreenPointToRay(screenPosition);
          unit.Execute(ray);
        }
      }
    }
  }
}
