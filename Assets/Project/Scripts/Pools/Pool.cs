using UnityEngine;
using UnityEngine.Pool;

namespace bts {
  public abstract class Pool<T> : MonoBehaviour
    where T : MonoBehaviour, Poolable {
    [SerializeField] PoolEventChannel<T> poolEventChannel;
    [SerializeField] T prefab;
    IObjectPool<T> pool;

    void Awake() {
      pool = new ObjectPool<T>(() =>{
        T obj = Instantiate(prefab, transform);
        obj.SetPool(pool);
        return obj;
      },
      (obj) => obj.gameObject.SetActive(true),
      (obj) => obj.gameObject.SetActive(false));
    }

    void OnEnable() {
      poolEventChannel.OnSpawnRequest += Spawn;
      poolEventChannel.OnGetRequest += Get;
    }

    void OnDisable() {
      poolEventChannel.OnSpawnRequest -= Spawn;
      poolEventChannel.OnGetRequest -= Get;
    }

    void Spawn(PooledObjectConfiguration<T> configuration, PooledObjectParameters<T> parameters) {
      _ = Get(configuration, parameters);
    }
    
    T Get(PooledObjectConfiguration<T> configuration, PooledObjectParameters<T> parameters) {
      T obj = pool.Get();
      configuration.ApplyTo(obj);
      parameters.SetTo(obj);
      return obj;
    }
  }
}
