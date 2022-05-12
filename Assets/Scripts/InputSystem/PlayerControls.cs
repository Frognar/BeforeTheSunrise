//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/InputControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""fd62e94d-83c1-4286-b165-1f3499de2bb0"",
            ""actions"": [
                {
                    ""name"": ""Zoom"",
                    ""type"": ""PassThrough"",
                    ""id"": ""4784b3e1-13ed-4fc2-a7e3-95362c02293e"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": ""Clamp(min=-1,max=1)"",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ScreenPosition"",
                    ""type"": ""Value"",
                    ""id"": ""1d6299bd-aeb0-43ef-991b-4878936f7e8d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""CameraRotationDirection"",
                    ""type"": ""Value"",
                    ""id"": ""7bd56e4e-59f8-4307-992d-8875c2de1f1f"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": ""Clamp(min=-1,max=1)"",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""293f248b-6d19-47c1-9d8a-e2c0907a2d17"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SendCommand"",
                    ""type"": ""Button"",
                    ""id"": ""23bf4e16-14a1-474b-b80a-9ba3da3c1fc0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""EnableCameraRotation"",
                    ""type"": ""Button"",
                    ""id"": ""9a81cf66-b65d-460d-9a6a-d47212795b37"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""EnableCommendQueuing"",
                    ""type"": ""Button"",
                    ""id"": ""aa97e346-a200-4bdd-bf79-1c31341603f1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""37a67a80-01c1-43e4-9d64-101b5c6361f6"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4100393e-97a4-4d2e-a888-d47c2362007f"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ScreenPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e9750149-7f7d-4749-8a11-a866f58988e3"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c3df95ed-b334-46dc-8aa7-5672ba6a9173"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SendCommand"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e80e9751-5cb8-4ced-983c-6939903df2f2"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EnableCameraRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b438ff8f-0da2-4341-96ac-e398d1cb4ec7"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraRotationDirection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a4e590ed-abc1-4fe1-9376-1682bc8e5470"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EnableCommendQueuing"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Zoom = m_Player.FindAction("Zoom", throwIfNotFound: true);
        m_Player_ScreenPosition = m_Player.FindAction("ScreenPosition", throwIfNotFound: true);
        m_Player_CameraRotationDirection = m_Player.FindAction("CameraRotationDirection", throwIfNotFound: true);
        m_Player_Select = m_Player.FindAction("Select", throwIfNotFound: true);
        m_Player_SendCommand = m_Player.FindAction("SendCommand", throwIfNotFound: true);
        m_Player_EnableCameraRotation = m_Player.FindAction("EnableCameraRotation", throwIfNotFound: true);
        m_Player_EnableCommendQueuing = m_Player.FindAction("EnableCommendQueuing", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Zoom;
    private readonly InputAction m_Player_ScreenPosition;
    private readonly InputAction m_Player_CameraRotationDirection;
    private readonly InputAction m_Player_Select;
    private readonly InputAction m_Player_SendCommand;
    private readonly InputAction m_Player_EnableCameraRotation;
    private readonly InputAction m_Player_EnableCommendQueuing;
    public struct PlayerActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Zoom => m_Wrapper.m_Player_Zoom;
        public InputAction @ScreenPosition => m_Wrapper.m_Player_ScreenPosition;
        public InputAction @CameraRotationDirection => m_Wrapper.m_Player_CameraRotationDirection;
        public InputAction @Select => m_Wrapper.m_Player_Select;
        public InputAction @SendCommand => m_Wrapper.m_Player_SendCommand;
        public InputAction @EnableCameraRotation => m_Wrapper.m_Player_EnableCameraRotation;
        public InputAction @EnableCommendQueuing => m_Wrapper.m_Player_EnableCommendQueuing;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Zoom.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnZoom;
                @Zoom.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnZoom;
                @Zoom.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnZoom;
                @ScreenPosition.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnScreenPosition;
                @ScreenPosition.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnScreenPosition;
                @ScreenPosition.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnScreenPosition;
                @CameraRotationDirection.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraRotationDirection;
                @CameraRotationDirection.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraRotationDirection;
                @CameraRotationDirection.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraRotationDirection;
                @Select.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelect;
                @SendCommand.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSendCommand;
                @SendCommand.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSendCommand;
                @SendCommand.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSendCommand;
                @EnableCameraRotation.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEnableCameraRotation;
                @EnableCameraRotation.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEnableCameraRotation;
                @EnableCameraRotation.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEnableCameraRotation;
                @EnableCommendQueuing.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEnableCommendQueuing;
                @EnableCommendQueuing.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEnableCommendQueuing;
                @EnableCommendQueuing.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEnableCommendQueuing;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Zoom.started += instance.OnZoom;
                @Zoom.performed += instance.OnZoom;
                @Zoom.canceled += instance.OnZoom;
                @ScreenPosition.started += instance.OnScreenPosition;
                @ScreenPosition.performed += instance.OnScreenPosition;
                @ScreenPosition.canceled += instance.OnScreenPosition;
                @CameraRotationDirection.started += instance.OnCameraRotationDirection;
                @CameraRotationDirection.performed += instance.OnCameraRotationDirection;
                @CameraRotationDirection.canceled += instance.OnCameraRotationDirection;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
                @SendCommand.started += instance.OnSendCommand;
                @SendCommand.performed += instance.OnSendCommand;
                @SendCommand.canceled += instance.OnSendCommand;
                @EnableCameraRotation.started += instance.OnEnableCameraRotation;
                @EnableCameraRotation.performed += instance.OnEnableCameraRotation;
                @EnableCameraRotation.canceled += instance.OnEnableCameraRotation;
                @EnableCommendQueuing.started += instance.OnEnableCommendQueuing;
                @EnableCommendQueuing.performed += instance.OnEnableCommendQueuing;
                @EnableCommendQueuing.canceled += instance.OnEnableCommendQueuing;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnZoom(InputAction.CallbackContext context);
        void OnScreenPosition(InputAction.CallbackContext context);
        void OnCameraRotationDirection(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
        void OnSendCommand(InputAction.CallbackContext context);
        void OnEnableCameraRotation(InputAction.CallbackContext context);
        void OnEnableCommendQueuing(InputAction.CallbackContext context);
    }
}
