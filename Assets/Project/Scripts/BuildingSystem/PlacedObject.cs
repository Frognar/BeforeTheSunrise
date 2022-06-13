using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace fro.BuildingSystem {
  public class PlacedObject : MonoBehaviour {
    public UnityEvent OnCreate;
    public UnityEvent OnDemolish;
    [SerializeField] PlacedObjectData placedObjectData;
    GridCords origin;

    void Awake() {
      origin = FindObjectOfType<GridBuildingSystem>().Grid.GetCords(transform.position);
      Invoke(nameof(Destroy), 4f);
    }

    void Destroy() {
      Destroy(gameObject);
    }

    void OnEnable() {
      OnCreate.Invoke();
    }

    void OnDisable() {
      OnDemolish.Invoke();
    }

    public void Demolish() {
      Destroy(gameObject);
    }

    public List<GridCords> GetGridPositions() {
      return placedObjectData.GetGridPositions(origin);
    }
  }
}
