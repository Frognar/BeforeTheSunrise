using UnityEngine;

namespace bts {
  public interface Selectable {
    Transform Transform { get; }
    void Select();
    void Deselect();
  }
}
