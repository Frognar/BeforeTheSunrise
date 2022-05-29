using UnityEngine;

namespace bts {
  public class PlaceableFactory : MonoBehaviour {
    Transform Parent => parent == null ? (parent = new GameObject("Buildings|Obstacles").transform) : parent;
    Transform parent;

    public Placeable Create(Vector3 worldPosition, Vector3Int origin, PlacedObjectType objectType, GridBuildingSystem gridBuildingSystem) {
      Transform placedObjectTransform = Instantiate(objectType.prefab, worldPosition, Quaternion.identity, Parent);
      Placeable placedObject = placedObjectTransform.GetComponent<Placeable>();
      Transform center = CreateObjectCenter(placedObject.Transform, new Vector3(objectType.width / 2, 0, objectType.height / 2));
      placedObject.Init(gridBuildingSystem, objectType, origin, center);
      return placedObject;
    }

    Transform CreateObjectCenter(Transform parent, Vector3 position) {
      Transform center = new GameObject("Center").transform;
      center.parent = parent;
      center.transform.localPosition = position;
      return center;
    }
  }
}
