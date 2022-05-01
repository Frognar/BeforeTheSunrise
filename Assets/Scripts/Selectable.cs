using UnityEngine;

namespace bts {
  public interface Selectable {
    Affiliation ObjectAffiliation { get; }
    Type ObjectType { get; }
    Transform Transform { get; }

    void Select();
    void Deselect();

    public enum Affiliation {
      Neutral = 1,
      Enemy = 2,
      Player = 4,
    }

    public enum Type {
      Unit = 1,
      Building = 2,
      Obstacle = 4,
    }
  }
}
