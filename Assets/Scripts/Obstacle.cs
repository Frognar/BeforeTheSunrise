using UnityEngine;

namespace bts {
  public class Obstacle : MonoBehaviour, Selectable {
    public void Select() {
      Debug.Log("Obstacle selected!");
    }
  }
}
