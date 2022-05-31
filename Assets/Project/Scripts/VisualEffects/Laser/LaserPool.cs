using UnityEngine;
using UnityEngine.Pool;

namespace bts {
  public class LaserPool : MonoBehaviour {
    [SerializeField] LaserEventChannel laserEventChannel;
    [SerializeField] Laser laserPrefab;

    IObjectPool<Laser> laserPool;

    void Awake() {
      laserPool = new ObjectPool<Laser>(() => {
        Laser laser = Instantiate(laserPrefab, transform);
        laser.pool = laserPool;
        return laser;
      },
      (laser) => laser.gameObject.SetActive(true),
      (laser) => laser.gameObject.SetActive(false));
    }

    void OnEnable() {
      laserEventChannel.OnLaserEventRequest += GetLaser;
    }

    void OnDisable() {
      laserEventChannel.OnLaserEventRequest -= GetLaser;
    }

    Laser GetLaser(Vector3 sourcePosition, Transform target, LaserConfiguration config) {
      Laser laser = laserPool.Get();
      laser.SetUp(sourcePosition, target, config);
      return laser;
    }
  }
}
