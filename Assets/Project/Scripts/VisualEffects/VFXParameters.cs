using UnityEngine;

namespace bts {
  public abstract class VFXParameters<T>
    where T : MonoBehaviour, Poolable {
    public abstract void SetTo(T vfx);
  }
}
