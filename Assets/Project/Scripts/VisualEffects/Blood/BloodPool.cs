using UnityEngine;
using UnityEngine.Pool;

namespace bts {
  public class BloodPool : MonoBehaviour {
    [SerializeField] BloodEventChannel bloodEventChannel;
    [SerializeField] Blood bloodPrefab;

    IObjectPool<Blood> bloodPool;

    void Awake() {
      bloodPool = new ObjectPool<Blood>(() => {
        Blood blood = Instantiate(bloodPrefab, transform);
        blood.pool = bloodPool;
        return blood;
      },
      (blood) => blood.gameObject.SetActive(true),
      (blood) => blood.gameObject.SetActive(false));
    }

    void OnEnable() {
      bloodEventChannel.OnBloodEventRequest += GetBlood;
    }

    void OnDisable() {
      bloodEventChannel.OnBloodEventRequest -= GetBlood;
    }
    
    Blood GetBlood(Vector3 position, BloodConfiguration config) {
      Blood blood = bloodPool.Get();
      blood.SetUp(position, config);
      return blood;
    }
  }
}
