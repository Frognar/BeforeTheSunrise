using UnityEngine;

namespace bts {
  public class Obstacle : MonoBehaviour, Selectable, Damageable {
    public void Select() {
      Debug.Log("Obstacle selected!");
    }

    public void TakeDamage(int amount) {
      Destroy(gameObject);
    }
  }
}
