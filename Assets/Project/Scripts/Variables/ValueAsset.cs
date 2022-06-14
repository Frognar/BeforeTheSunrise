using UnityEngine;

namespace fro.ValueAssets {
  public abstract class ValueAsset<T> : ScriptableObject {
    public T value;
    public static implicit operator T(ValueAsset<T> asset) => asset.value;
  }
}
