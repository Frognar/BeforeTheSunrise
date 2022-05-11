using UnityEngine;

namespace bts {
  public interface Damageable {
    Transform Transform { get; }
    Affiliation ObjectAffiliation { get; }
    Vector3 Position { get; }
    bool IsDead { get; }
    void TakeDamage(int amount);
  }
}
