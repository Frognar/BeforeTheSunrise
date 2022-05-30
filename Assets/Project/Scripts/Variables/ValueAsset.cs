using UnityEngine;

namespace bts {
  public abstract class ValueAsset<T> : ScriptableObject {
    public T value;
    public static implicit operator T(ValueAsset<T> asset) => asset.value;
  }
}
