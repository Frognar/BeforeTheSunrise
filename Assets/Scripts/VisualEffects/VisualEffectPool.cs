using UnityEngine;
using UnityEngine.Pool;

namespace bts {
  public class VisualEffectPool : MonoBehaviour {
    [SerializeField] VFXEventChannel vfxEventChannel;
    [SerializeField] ElectricArcVFX electricArcPrefab;

    IObjectPool<ElectricArcVFX> electricArcsPool;

    void Awake() {
      electricArcsPool = new ObjectPool<ElectricArcVFX>(() => {
        ElectricArcVFX electricArc = Instantiate(electricArcPrefab, transform);
        electricArc.pool = electricArcsPool;
        return electricArc;
      },
      (electricArc) => electricArc.gameObject.SetActive(true),
      (electricArc) => electricArc.gameObject.SetActive(false));
    }
    
    void OnEnable() {
      vfxEventChannel.OnVFXEventRequest += GetVFX;
    }

    void OnDisable() {
      vfxEventChannel.OnVFXEventRequest -= GetVFX;
    }

    void GetVFX(Transform source, Vector3 targetPosition, ElectricArcVFXConfiguration config) {
      ElectricArcVFX electricArc = electricArcsPool.Get();
      electricArc.transform.position = source.position;
      electricArc.SetUp(source, targetPosition, config);
    }
  }
}
