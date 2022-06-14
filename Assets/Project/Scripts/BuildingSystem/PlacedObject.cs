using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace fro.BuildingSystem {
  public class PlacedObject : MonoBehaviour {
    public UnityEvent OnCreate;
    public UnityEvent OnDemolish;
    [SerializeField] PlacedObjectData placedObjectData;
    GridCords origin;

    public void Init(GridCords cords) {
      origin = cords;
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
