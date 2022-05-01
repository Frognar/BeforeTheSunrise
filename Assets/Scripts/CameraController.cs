using Cinemachine;
using UnityEngine;

namespace bts {
  public class CameraController : MonoBehaviour {
    [SerializeField][Range(15f, 50f)] float panSpeed;
    [SerializeField][Range(15f, 50f)] float zoomSpeed;
    [SerializeField] Vector2 panLimitX;
    [SerializeField] Vector2 panLimitZ;
    [SerializeField] Vector2 zoomLimit;
    CinemachineInputProvider inputProvider;

    void Awake() {
      inputProvider = GetComponent<CinemachineInputProvider>();
    }

    void Update() {
      HandlePan();
      HandleZoom();
    }

    void HandlePan() {
      float x = inputProvider.GetAxisValue(0);
      float y = inputProvider.GetAxisValue(1);
      if (x != 0 || y != 0) {
        Pan(x, y);
      }
    }
    
    void Pan(float x, float y) {
      Vector3 direction = CalculatePanDirection(x, y);
      Vector3 newPosition = transform.localPosition + direction;
      newPosition.x = Mathf.Clamp(newPosition.x, -panLimitX.x, panLimitX.y);
      newPosition.z = Mathf.Clamp(newPosition.z, -panLimitZ.x, panLimitZ.y);
      transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, Time.deltaTime * panSpeed);
    }

    Vector3 CalculatePanDirection(float x, float y) {
      Vector3 direction = Vector3.zero;
      if (x >= Screen.width * .95f) {
        direction.x = 1;
      }
      else if (x <= Screen.width * .05f) {
        direction.x = -1;
      }

      if (y >= Screen.height * .95f) {
        direction.z = 1;
      }
      else if (y <= Screen.height * .05f) {
        direction.z = -1;
      }

      return direction;
    }

    void HandleZoom() {
      float scroll = inputProvider.GetAxisValue(2);
      if (scroll != 0) {
        Zoom(scroll);
      }
    }

    void Zoom(float scroll) {
      Vector3 newPosition = transform.localPosition + transform.forward * scroll;
      if (newPosition.y > zoomLimit.x && newPosition.y < zoomLimit.y) {
        transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, Time.deltaTime * zoomSpeed);
      }
    }
  }
}
