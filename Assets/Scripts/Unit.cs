using UnityEngine;

namespace bts {
  public class Unit : MonoBehaviour, Selectable {
    public void Select() {
      Debug.Log("Unit selected!");
    }
  }
}
