using UnityEngine;

namespace fro.BuildingSystem {
  public class TestBuilder : MonoBehaviour {
    [SerializeField] PlacedObjectData placedObjectData;
    [SerializeField] GridBuildingSystem gridBuildingSystem;

    void Start() {
      gridBuildingSystem.Build(Vector3.zero, placedObjectData);
      gridBuildingSystem.Build(Vector3.one, placedObjectData); // Can't build

      Invoke(nameof(Clear), 1f);
      Invoke(nameof(Build), 8f);
    }

    void Clear() {
      gridBuildingSystem.Demolish(Vector3.one);
      Invoke(nameof(Build), 1f);
    }

    void Build() {
      gridBuildingSystem.Build(Vector3.one, placedObjectData);
    }
  }
}
