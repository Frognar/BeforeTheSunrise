using UnityEngine;

namespace fro.BuildingSystem {
  public class PlacedObjectFactory : MonoBehaviour {
    public PlacedObject Create(GridCords gridPosition, PlacedObjectData objectData, GridBuildingSystem buildingSystem) {
      PlacedObject placedObject = Instantiate(objectData.Prefab, buildingSystem.Grid.GetWorldPosition(gridPosition), Quaternion.identity, buildingSystem.transform);
      placedObject.Init(gridPosition);
      return placedObject;
    }
  }
}
