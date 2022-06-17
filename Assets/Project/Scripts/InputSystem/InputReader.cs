using System;
using fro.ValueAssets;
using UnityEngine;
using UnityEngine.InputSystem;

namespace bts {
  [CreateAssetMenu(fileName = "Input Reader", menuName = "Game/Input Reader")]
  public class InputReader : ScriptableObject, PlayerControls.IPlayerActions, PlayerControls.ICameraActions, PlayerControls.IMenuActions {
    [SerializeField] BoolAsset inBuildMode;
    public event Action CancelEvent = delegate { };
    public bool IsCommandQueuingEnabled;
    public event Action<Vector3> StartSelectiongEvent = delegate { };
    public event Action<Vector3> StopSelectiongEvent = delegate { };
    public event Action<Vector3> SendBuildCommandEvent = delegate { };
    public event Action<Ray> SendCommandEvent = delegate { };
    public bool IsCameraRotationEnable { get; private set; }
    public event Action<float> CameraRotationEvent = delegate { };
    public event Action FocusEvent = delegate { };
    public event Action<Vector3> WorldPositionEvent = delegate { };
    public event Action<Vector2> ScreenPositionEvent = delegate { };
    public Vector2 ScreenPosition;
    Ray RayToWorld => Camera.main != null ? Camera.main.ScreenPointToRay(ScreenPosition) : new Ray();
    static Vector3 offWorld = new Vector3(-10000, -10000, -10000);
    public Vector3 WorldPosition => Physics.Raycast(RayToWorld, out RaycastHit hitInfo) ? hitInfo.point : offWorld;
    public event Action<float> ZoomEvent = delegate { };
    public event Action<Ray> SelectSameEvent = delegate { };
    public event Action OnPauseEvent = delegate { };
    public event Action OnCloseEvent = delegate { };

    PlayerControls inputControls;

    void OnEnable() {
      if (inputControls == null) {
        inputControls = new PlayerControls();
        inputControls.Player.SetCallbacks(this);
        inputControls.Camera.SetCallbacks(this);
        inputControls.Menu.SetCallbacks(this);
      }
    }

    void OnDisable() {
      inputControls.Player.Disable();
      inputControls.Camera.Disable();
      inputControls.Menu.Disable();
    }

    public void OnCancel(InputAction.CallbackContext context) {
      if (context.performed) {
        CancelEvent.Invoke();
      }
    }

    public void OnEnableCommendQueuing(InputAction.CallbackContext context) {
      switch (context.phase) {
        case InputActionPhase.Performed:
          IsCommandQueuingEnabled = true;
          break;
        case InputActionPhase.Canceled:
          IsCommandQueuingEnabled = false;
          break;
      }
    }

    public void OnSelectSame(InputAction.CallbackContext context) {
      if (!inBuildMode) {
        if (context.performed) {
          SelectSameEvent.Invoke(RayToWorld);
        }
      }
    }

    public void OnStartSelecting(InputAction.CallbackContext context) {
      if (context.performed) {
        if (!inBuildMode) {
          StartSelectiongEvent.Invoke(WorldPosition);
        }
        else {
          SendBuildCommandEvent.Invoke(WorldPosition);
        }
      }
    }

    public void OnStopSelecting(InputAction.CallbackContext context) {
      if (context.performed) {
        StopSelectiongEvent.Invoke(WorldPosition);
      }
    }

    public void OnSendCommand(InputAction.CallbackContext context) {
      if (context.performed) {
        SendCommandEvent.Invoke(RayToWorld);
      }
    }

    public void OnCameraRotationDirection(InputAction.CallbackContext context) {
      if (IsCameraRotationEnable) {
        CameraRotationEvent.Invoke(context.ReadValue<float>());
      }
    }

    public void OnEnableCameraRotation(InputAction.CallbackContext context) {
      switch (context.phase) {
        case InputActionPhase.Performed:
          IsCameraRotationEnable = true;
          break;
        case InputActionPhase.Canceled:
          IsCameraRotationEnable = false;
          break;
      }
    }

    public void OnFocus(InputAction.CallbackContext context) {
      if (context.performed) {
        FocusEvent.Invoke();
      }
    }

    public void OnScreenPosition(InputAction.CallbackContext context) {
      ScreenPosition = context.ReadValue<Vector2>();
      ScreenPositionEvent.Invoke(ScreenPosition);
      WorldPositionEvent.Invoke(WorldPosition);
    }

    public void OnZoom(InputAction.CallbackContext context) {
      ZoomEvent.Invoke(context.ReadValue<float>());
    }

    public void OnPause(InputAction.CallbackContext context) {
      if (context.performed) {
        OnPauseEvent.Invoke();
      }
    }

    public void OnClose(InputAction.CallbackContext context) {
      if (context.performed) {
        OnCloseEvent.Invoke();
      }
    }

    public void EnableGameplayInput() {
      inputControls.Player.Enable();
      inputControls.Camera.Enable();
    }

    public void DisableMenuInput() {
      inputControls.Menu.Disable();
    }

    public void EnableMenuInput() {
      inputControls.Menu.Enable();
    }

    public void DisableGameplayInput() {
      inputControls.Player.Disable();
      inputControls.Camera.Disable();
      ScreenPosition = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    public class GroupSelectionEventArgs : EventArgs {
      public int GroupId { get; }
      public bool ShouldSave { get; }

      public GroupSelectionEventArgs(int groupId, bool shouldSave) {
        GroupId = groupId;
        ShouldSave = shouldSave;
      }
    }

    public event EventHandler<GroupSelectionEventArgs> GroupSelectionEvent = delegate { };
    bool saveSelection;

    public void OnSaveSelectionGroup(InputAction.CallbackContext context) {
      if (context.performed) {
        saveSelection = true;
      }
      else if (context.canceled) {
        saveSelection = false;
      }
    }

    public void OnSelectionGroup1(InputAction.CallbackContext context) {
      if (context.performed) {
        GroupSelectionEvent.Invoke(this, new GroupSelectionEventArgs(1, saveSelection));
      }
    }

    public void OnSelectionGroup2(InputAction.CallbackContext context) {
      if (context.performed) {
        GroupSelectionEvent.Invoke(this, new GroupSelectionEventArgs(2, saveSelection));
      }
    }

    public void OnSelectionGroup3(InputAction.CallbackContext context) {
      if (context.performed) {
        GroupSelectionEvent.Invoke(this, new GroupSelectionEventArgs(3, saveSelection));
      }
    }

    public void OnSelectionGroup4(InputAction.CallbackContext context) {
      if (context.performed) {
        GroupSelectionEvent.Invoke(this, new GroupSelectionEventArgs(4, saveSelection));
      }
    }

    public void OnSelectionGroup5(InputAction.CallbackContext context) {
      if (context.performed) {
        GroupSelectionEvent.Invoke(this, new GroupSelectionEventArgs(5, saveSelection));
      }
    }

    public void OnSelectionGroup6(InputAction.CallbackContext context) {
      if (context.performed) {
        GroupSelectionEvent.Invoke(this, new GroupSelectionEventArgs(6, saveSelection));
      }
    }

    public void OnSelectionGroup7(InputAction.CallbackContext context) {
      if (context.performed) {
        GroupSelectionEvent.Invoke(this, new GroupSelectionEventArgs(7, saveSelection));
      }
    }

    public void OnSelectionGroup8(InputAction.CallbackContext context) {
      if (context.performed) {
        GroupSelectionEvent.Invoke(this, new GroupSelectionEventArgs(8, saveSelection));
      }
    }

    public void OnSelectionGroup9(InputAction.CallbackContext context) {
      if (context.performed) {
        GroupSelectionEvent.Invoke(this, new GroupSelectionEventArgs(9, saveSelection));
      }
    }

    public void OnSelectionGroup10(InputAction.CallbackContext context) {
      if (context.performed) {
        GroupSelectionEvent.Invoke(this, new GroupSelectionEventArgs(0, saveSelection));
      }
    }
  }
}