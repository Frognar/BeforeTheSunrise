using System.Collections.Generic;
using UnityEngine;

namespace bts {
  public interface Placeable {
    GridBuildingSystem GridBuildingSystem { get; }
    PlacedObjectTypeSO PlaceableObjectType { get; }
    Vector3Int Origin { get; }
    Collider Obstacle { get; }
    Transform Center { get; }
    Transform Transform { get; }
    void Init(GridBuildingSystem gridBuildingSystem, PlacedObjectTypeSO objectType, Vector3Int origin, Transform center);

    List<Vector3Int> GetGridPositions() {
      return PlaceableObjectType.GetGridPositions(Origin);
    }

    void Demolish();
  }
}
