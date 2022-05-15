using System.Linq;
using UnityEngine;

namespace bts {
  public class CameraController : MonoBehaviour {
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
    Player player;
    Selectable selected;

    void Awake() {
      playerInputs = FindObjectOfType<PlayerInputs>();
      cameraTransform = Camera.main.transform;
      player = FindObjectOfType<Player>();
    }

    void OnEnable() {
      player.OnSelection += SelectionChanged;
    }

    void OnDisable() {
      player.OnSelection -= SelectionChanged;
    }

    void SelectionChanged(object sender, Player.OnSelectionEventArgs e) {
      selected = null;
      if (e.Selected.Count > 0) {
        selected = e.Selected.First();
      }
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
        if (screenPosition.x >= Screen.width * .95f) {
          newPosition += transform.right * movementSpeed;
        }
        else if (screenPosition.x <= Screen.width * .05f) {
          newPosition += transform.right * -movementSpeed;
        }

        if (screenPosition.y >= Screen.height * .95f) {
          newPosition += transform.forward * movementSpeed;
        }
        else if (screenPosition.y <= Screen.height * .05f) {
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
      if (playerInputs.Focus && selected != null) {
        Vector3 selectedPosition = selected.Transform.position;
        newPosition.x = selectedPosition.x;
        newPosition.z = selectedPosition.z;
      }
    }
  }
}
