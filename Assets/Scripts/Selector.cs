using UnityEngine;
using UnityEngine.InputSystem;

namespace bts {
  [RequireComponent(typeof(PlayerInput))]
  public class Selector : MonoBehaviour {
    public void OnScreenPosition(InputValue value) {
      Debug.Log(value.Get<Vector2>());
    }
  }
}
