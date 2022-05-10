using UnityEngine;

namespace bts {
  public interface Damageable {
    Affiliation ObjectAffiliation { get; }
    Vector3 Position { get; }
    void TakeDamage(int amount);
  }
}
