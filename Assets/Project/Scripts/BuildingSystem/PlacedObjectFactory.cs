using UnityEngine;

namespace fro.BuildingSystem {
  public class PlacedObjectFactory : MonoBehaviour {
    public PlacedObject Create(GridCords gridPosition, PlacedObjectData objectData, GridBuildingSystem buildingSystem) {
      return Instantiate(objectData.Prefab, buildingSystem.Grid.GetWorldPosition(gridPosition), Quaternion.identity, buildingSystem.transform);
    }
  }
}
