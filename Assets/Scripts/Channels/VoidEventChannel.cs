using System;
using UnityEngine;

namespace bts {
  [CreateAssetMenu(fileName = "Void Channel", menuName = "Channels/Void Channel")]
  public class VoidEventChannel : ScriptableObject {
    public event EventHandler OnEventInvoked;

    public void Invoke() {
      OnEventInvoked?.Invoke(this, EventArgs.Empty);
    }
  }
}
