using UnityEngine;

namespace bts {
  public interface Healable {
    Transform Center { get; }
    Affiliation ObjectAffiliation { get; }
    bool IsIntact { get; }
    void Heal(float amount);
  }
}
