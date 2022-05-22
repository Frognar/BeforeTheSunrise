using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace bts {
  public class CameraController : MonoBehaviour {
    const int pixelsFromScreenEdge = 1;

    [SerializeField] SelectablesEventChannel selectablesEventChannel;
    [SerializeField][Range(1f, 5f)] float movementSpeed;
    [SerializeField][Range(1f, 5f)] float movementTime;
    [SerializeField][Range(5f, 15f)] float rotationAmount;
    [SerializeField] Vector3 zoomAmount;
    [SerializeField] Vector2 positionLimitX;
    [SerializeField] Vector2 positionLimitZ;
    [SerializeField] Vector2 zoomLimits;

    Vector3 newPosition;
    Quaternion newRotation;
    Vector3 newZoom;
    Transform cameraTransform;
    PlayerInputs playerInputs;
    Selectable focus;

    void Awake() {
      playerInputs = FindObjectOfType<PlayerInputs>();
      cameraTransform = Camera.main.transform;
    }

    void OnEnable() {
      selectablesEventChannel.OnSelectionInvoked += SelectionChanged;
    }

    void OnDisable() {
      selectablesEventChannel.OnSelectionInvoked -= SelectionChanged;
    }

    void SelectionChanged(object sender, List<Selectable> selected) {
      focus = (selected.Count > 0) ? selected.First() : null;
    }

    void Start() {
      newPosition = transform.position;
      newRotation = transform.rotation;
      newZoom = cameraTransform.localPosition;
    }

    void Update() {
      HandleMovement();
      HandleRotation();
      HandleZoom();
      HandleFocus();
    }

    void HandleMovement() {
      if (!playerInputs.IsCameraRotationEnable) {
        Vector2 screenPosition = playerInputs.ScreenPosition;
        if (screenPosition.x >= Screen.width - pixelsFromScreenEdge) {
          newPosition += transform.right * movementSpeed;
        }
        else if (screenPosition.x <= pixelsFromScreenEdge) {
          newPosition += transform.right * -movementSpeed;
        }

        if (screenPosition.y >= Screen.height - pixelsFromScreenEdge) {
          newPosition += transform.forward * movementSpeed;
        }
        else if (screenPosition.y <= pixelsFromScreenEdge) {
          newPosition += transform.forward * -movementSpeed;
        }

        newPosition.x = Mathf.Clamp(newPosition.x, positionLimitX.x, positionLimitX.y);
        newPosition.z = Mathf.Clamp(newPosition.z, positionLimitZ.x, positionLimitZ.y);
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
      }
    }

    void HandleRotation() {
      if (playerInputs.IsCameraRotationEnable) {
        float screenPositionXDelta = playerInputs.CameraRotationDirection;
        if (screenPositionXDelta is > 0.95f) {
          newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        else if(screenPositionXDelta is < -0.95f) {
          newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationAmount);
      }
    }

    void HandleZoom() {
      float zoom = playerInputs.Zoom;

      if (zoom > 0f) {
        if (newZoom.y > zoomLimits.x) {
          newZoom -= zoomAmount;
        }
      }
      else if (zoom < 0f) {
        if (newZoom.y < zoomLimits.y) {
          newZoom += zoomAmount;
        }
      }

      cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }

    void HandleFocus() {
      if (playerInputs.Focus && focus != null) {
        Vector3 selectedPosition = focus.Transform.position;
        newPosition.x = selectedPosition.x;
        newPosition.z = selectedPosition.z;
      }
    }
  }
}
