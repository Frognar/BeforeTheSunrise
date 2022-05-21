using System;
using System.Collections.Generic;
using UnityEngine;

namespace bts {
  [CreateAssetMenu(fileName = "Selectables Channel", menuName = "Channels/Selectables Channel")]
  public class SelectablesEventChannel : ScriptableObject {
    public event EventHandler<List<Selectable>> OnSelectionInvoked;
    public event EventHandler<Selectable> OnDeselect;
    
    public void Invoke(List<Selectable> selected) {
      OnSelectionInvoked?.Invoke(this, selected);
    }

    public void Invoke(Selectable selected) {
      OnDeselect?.Invoke(this, selected);
    }
  }
}
