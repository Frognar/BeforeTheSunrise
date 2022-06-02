using UnityEngine;
using UnityEngine.Pool;

namespace bts {
  public abstract class VFXPool<T> : MonoBehaviour
    where T : MonoBehaviour, Poolable {
    [SerializeField] VFXEventChannel<T> vfxEventChannel;
    [SerializeField] T prefab;
    IObjectPool<T> vfxPool;

    void Awake() {
      vfxPool = new ObjectPool<T>(() =>{
        T vfx = Instantiate(prefab, transform);
        vfx.SetPool(vfxPool);
        return vfx;
      },
      (vfx) => vfx.gameObject.SetActive(true),
      (vfx) => vfx.gameObject.SetActive(false));
    }

    void OnEnable() {
      vfxEventChannel.OnVFXEventRequest += GetVFX;
      vfxEventChannel.OnVFXEventRequestWithReference += GetVFXWithReference;
    }

    void OnDisable() {
      vfxEventChannel.OnVFXEventRequest -= GetVFX;
      vfxEventChannel.OnVFXEventRequestWithReference -= GetVFXWithReference;
    }

    void GetVFX(VFXConfiguration<T> configuration, VFXParameters<T> parameters) {
      _ = GetVFXWithReference(configuration, parameters);
    }
    
    T GetVFXWithReference(VFXConfiguration<T> configuration, VFXParameters<T> parameters) {
      T vfx = vfxPool.Get();
      configuration.ApplyTo(vfx);
      parameters.SetTo(vfx);
      return vfx;
    }
  }
}
