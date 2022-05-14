using UnityEngine;
using UnityEngine.InputSystem;

namespace bts {
  public class PlayerInputs : MonoBehaviour, PlayerControls.IPlayerActions {
    [SerializeField] BoolVariable inBuildMode;
    public bool StartSelecting { get; private set; }
    public bool StopSelecting { get; private set; }
    public bool SendCommand { get; private set; }
    public bool SendBuildCommand { get; private set; }
    public bool IsCameraRotationEnable { get; private set; }
    public Vector2 ScreenPosition { get; private set; }
    public Ray RayToWorld => mainCamera.ScreenPointToRay(ScreenPosition);
    public Vector3 WorldPosition => Physics.Raycast(RayToWorld, out RaycastHit hitInfo) ? hitInfo.point : Vector3.zero;
    public float CameraRotationDirection { get; private set; }
    public float Zoom { get; private set; }
    public bool IsCommandQueuingEnabled { get; private set;}

    Camera mainCamera;
    PlayerControls inputControls;

    void Awake() {
      mainCamera = Camera.main;
      inputControls = new PlayerControls();
      inputControls.Player.SetCallbacks(this);
    }

    void OnEnable() {
      inputControls.Enable();
    }

    void OnDisable() {
      inputControls.Disable();
    }

    void LateUpdate() {
      SendBuildCommand = false;
      SendCommand = false;
      StartSelecting = false;
      StopSelecting = false;
    }

    public void OnScreenPosition(InputAction.CallbackContext context) {
      ScreenPosition = context.ReadValue<Vector2>();
    }

    public void OnZoom(InputAction.CallbackContext context) {
      Zoom = context.ReadValue<float>();
    }

    public void OnCameraRotationDirection(InputAction.CallbackContext context) {
      CameraRotationDirection = context.ReadValue<float>();
    }

    public void OnSelect(InputAction.CallbackContext context) {
      if (context.started) {
        if (inBuildMode) {
          SendBuildCommand = true;
        }
        else {
          StartSelecting = true;
        }
      }
      else if (context.canceled) {
        StopSelecting = true;
      }
    }

    public void OnSendCommand(InputAction.CallbackContext context) {
      if (context.started) {
        SendCommand = true;
      }
    }

    public void OnEnableCameraRotation(InputAction.CallbackContext context) {
      if (context.started) {
        IsCameraRotationEnable = true;
      }
      else if (context.canceled) {
        IsCameraRotationEnable = false;
      }
    }

    public void OnEnableCommendQueuing(InputAction.CallbackContext context) {
      if (context.started) {
        IsCommandQueuingEnabled = true;
      }
      else if (context.canceled) {
        IsCommandQueuingEnabled = false;
      }
    }
  }
}
