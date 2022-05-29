using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace bts {
  public interface Selectable {
    Dictionary<string, object> GetData();
    string Name { get; }
    Affiliation ObjectAffiliation { get; }
    Type ObjectType { get; }
    Transform Center { get; }
    GameObject Selected { get; }
    IEnumerable<UICommand> UICommands => Enumerable.Empty<UICommand>();
    SelectablesEventChannel SelectablesEventChannel { get; }
    void Select();
    void Deselect();
    bool IsSameAs(Selectable other);
  }
}
