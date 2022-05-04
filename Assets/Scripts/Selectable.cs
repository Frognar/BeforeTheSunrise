using UnityEngine;

namespace bts {
  public interface Selectable {
    Affiliation ObjectAffiliation { get; }
    Type ObjectType { get; }
    Transform Transform { get; }
    GameObject Selected { get; }

    public void Select() {
      Selected.SetActive(true);
    }

    public void Deselect() {
      Selected.SetActive(false);
    }
  }

  public enum Type {
    Unit = 1,
    Building = 2,
    Obstacle = 4,
  }

  public enum Affiliation {
    Neutral = 1,
    Enemy = 2,
    Player = 4,
  }
}
