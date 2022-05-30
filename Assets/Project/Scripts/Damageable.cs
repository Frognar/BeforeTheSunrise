using UnityEngine;

namespace bts {
  public interface Damageable {
    Transform Center { get; }
    Affiliation ObjectAffiliation { get; }
    Vector3 Position { get; }
    Bounds Bounds { get; }
    bool IsDead { get; }
    bool IsIntact { get; }
    void TakeDamage(float amount);
    void Heal(float amount);
  }
}
