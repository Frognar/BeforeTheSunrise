using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace bts {
  public interface Selectable {
    public event Action<Dictionary<DataType, object>> OnDataChange;
    Dictionary<DataType, object> GetData();
    Affiliation ObjectAffiliation { get; }
    Type ObjectType { get; }
    Transform Center { get; }
    Sprite Icon { get; }
    IEnumerable<UICommand> UICommands => Enumerable.Empty<UICommand>();
    SelectablesEventChannel SelectablesEventChannel { get; }
    void Select();
    void Deselect();
    bool IsSameAs(Selectable other);
  }
}
