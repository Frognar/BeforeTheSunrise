using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace bts {
  public interface Selectable {
    string Name { get; }
    Affiliation ObjectAffiliation { get; }
    Type ObjectType { get; }
    Transform Transform { get; }
    GameObject Selected { get; }
    IEnumerable<ObjectAction> Actions => Enumerable.Empty<ObjectAction>();
    void Select();
    void Deselect();
  }
}
