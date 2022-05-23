using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace bts {
  public class CameraController : MonoBehaviour {
    const int pixelsFromScreenEdge = 1;

    [SerializeField] InputReader inputReader;
    [SerializeField] SelectablesEventChannel selectablesEventChannel;
    [SerializeField][Range(1f, 5f)] float movementSpeed;
    [SerializeField][Range(1f, 5f)] float movementTime;
    [SerializeField][Range(5f, 15f)] float rotationAmount;
    [SerializeField] Vector3 zoomAmount;
    [SerializeField] IntAsset mapWidth;
    [SerializeField] IntAsset mapHeight;
    [SerializeField] Vector2 zoomLimits;

    Vector3 newPosition;
    Quaternion newRotation;
    Vector3 newZoom;
    Transform cameraTransform;
    Selectable focus;
    Vector2 positionLimitX;
    Vector2 positionLimitZ;

    void Awake() {
      cameraTransform = Camera.main.transform;
      positionLimitX = new Vector2(-mapWidth / 2f, mapWidth / 2f);
      positionLimitZ = new Vector2(-mapHeight / 2f, mapHeight / 2f);
    }

    void OnEnable() {
      selectablesEventChannel.OnSelectionInvoked += SelectionChanged;
      inputReader.FocusEvent += HandleFocus;
      inputReader.ZoomEvent += HandleZoom;
      inputReader.CameraRotationEvent += HandleRotation;
    }

    void OnDisable() {
      selectablesEventChannel.OnSelectionInvoked -= SelectionChanged;
      inputReader.FocusEvent -= HandleFocus;
      inputReader.ZoomEvent -= HandleZoom;
      inputReader.CameraRotationEvent -= HandleRotation;
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
      HandleMovement(inputReader.ScreenPosition);
      newPosition.x = Mathf.Clamp(newPosition.x, positionLimitX.x, positionLimitX.y);
      newPosition.z = Mathf.Clamp(newPosition.z, positionLimitZ.x, positionLimitZ.y);
      transform.SetPositionAndRotation(
        Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime),
        Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationAmount)
      );
      cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }

    void HandleMovement(Vector2 screenPosition) {
      if (!inputReader.IsCameraRotationEnable) {
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
      }
    }

    void HandleRotation(float screenPositionXDelta) {
      if (screenPositionXDelta is > 0.95f) {
        newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
      }
      else if (screenPositionXDelta is < -0.95f) {
        newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
      }
    }

    void HandleZoom(float zoom) {
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
    }

    void HandleFocus() {
      if (focus != null) {
        Vector3 selectedPosition = focus.Transform.position;
        newPosition.x = selectedPosition.x;
        newPosition.z = selectedPosition.z;
      }
    }
  }
}
