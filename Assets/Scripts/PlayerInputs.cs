using UnityEngine;
using UnityEngine.InputSystem;

namespace bts {
  public class PlayerInputs : MonoBehaviour, PlayerControls.IMouseActions {
    public bool IsLeftBtnDawn { get; private set; }
    public bool IsLeftBtnPressed { get; private set; }
    public bool IsLeftBtnUp { get; private set; }
    public bool IsRightBtnDawn { get; private set; }
    public bool IsMiddleBtnPressed { get; private set; }
    public Vector2 MouseScreenPosition { get; private set; }
    public float MouseScreenXDelta { get; private set; }
    public Vector3 MouseWorldPosition { get; private set; }
    public float Zoom { get; private set; }

    Camera mainCamera;
    PlayerControls inputControls;

    void Awake() {
      mainCamera = Camera.main;
      inputControls = new PlayerControls();
      inputControls.Mouse.SetCallbacks(this);
    }
    void OnEnable() {
      inputControls.Enable();
    }

    void OnDisable() {
      inputControls.Disable();
    }

    void LateUpdate() {
      IsLeftBtnDawn = false;
      IsLeftBtnUp = false;
      IsRightBtnDawn = false;
    }

    public void OnLeftClick(InputAction.CallbackContext context) {
      if (context.started) {
        IsLeftBtnDawn = true;
        IsLeftBtnPressed = true;
        IsLeftBtnUp = false;
      }
      else if (context.canceled) {
        IsLeftBtnDawn = false;
        IsLeftBtnPressed = false;
        IsLeftBtnUp = true;
      }
    }

    public void OnRightClick(InputAction.CallbackContext context) {
      if (context.started) {
        IsRightBtnDawn = true;
      }
    }

    public void OnScreenPosition(InputAction.CallbackContext context) {
      MouseScreenPosition = context.ReadValue<Vector2>();
    }

    public Vector3 GetMouseWorldPosition() {
      return GetWorldPosition(MouseScreenPosition);
    }

    public Vector3 GetWorldPosition(Vector2 screenPosition) {
      Ray ray = mainCamera.ScreenPointToRay(screenPosition);
      return Physics.Raycast(ray, out RaycastHit hitInfo) ? hitInfo.point : Vector3.zero;
    }

    public void OnZoom(InputAction.CallbackContext context) {
      Zoom = context.ReadValue<float>();
    }

    public void OnMiddleClick(InputAction.CallbackContext context) {
      if (context.started) {
        IsMiddleBtnPressed = true;
      }
      else if (context.canceled) {
        IsMiddleBtnPressed = false;
      }
    }

    public void OnScreenPositionXChange(InputAction.CallbackContext context) {
      MouseScreenXDelta = context.ReadValue<float>();
    }
  }
}
