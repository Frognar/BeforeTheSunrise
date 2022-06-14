using fro.BuildingSystem;
using fro.ValueAssets;
using UnityEngine;

namespace bts {
  public class GhostObject : MonoBehaviour {
    [SerializeField] InputReader inputReader;
    [SerializeField] BoolAsset canBuild;
    [SerializeField] Material canPlaceMaterial;
    [SerializeField] Material cantPlaceMaterial;
    [SerializeField] MeshFilter meshFilter;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] GameObject meshObject;
    [SerializeField] Transform rangeVisuals;
    [SerializeField] BoxCollider boxCollider;
    GridBuildingSystem buildingSystem;
    GridXZ<GridPlacedObject> grid;
    PlacedObjectData objectType;

    void Awake() {
      buildingSystem = FindObjectOfType<GridBuildingSystem>();
      grid = buildingSystem.Grid;
    }

    void OnEnable() {
      inputReader.WorldPositionEvent += UpdatePosition;
    }

    void OnDisable() {
      inputReader.WorldPositionEvent -= UpdatePosition;
    }

    void UpdatePosition(Vector3 worldPosition) {
      GridCords cords = grid.GetCords(worldPosition);
      transform.position = grid.GetWorldPosition(cords);
      canBuild.value = buildingSystem.CanBuild(transform.position, objectType);
      UpdateMaterial();
    }
    
    void UpdateMaterial() {
      meshRenderer.material = canBuild ? canPlaceMaterial : cantPlaceMaterial;
    }

    public void SetUp(PlacedObjectData buildingType, CustomBuildingData buildingData) {
      objectType = buildingType;
      boxCollider.size = new Vector3(buildingType.Width - .1f, .5f, buildingType.Height - .1f);
      meshFilter.mesh = buildingData.ghostMesh;
      meshObject.transform.localPosition = new Vector3(buildingType.Width / 2f, 0, buildingType.Height / 2f);
      UpdateMaterial();
      if (buildingData is GeneratorData generatorData) {
        rangeVisuals.gameObject.SetActive(true);
        float range = generatorData.range * 2f;
        rangeVisuals.localScale = new Vector3(range, range, 1f);
      }
      else if (buildingData is CannonData cannonData) {
        rangeVisuals.gameObject.SetActive(true);
        float range = cannonData.range * 2f;
        rangeVisuals.localScale = new Vector3(range, range, 1f);
      }
      else if (buildingData is HealerData healerData) {
        rangeVisuals.gameObject.SetActive(true);
        float range = healerData.range * 2f;
        rangeVisuals.localScale = new Vector3(range, range, 1f);
      }
      else if (buildingData is AuraData auraData) {
        rangeVisuals.gameObject.SetActive(true);
        float range = auraData.range * 2f;
        rangeVisuals.localScale = new Vector3(range, range, 1f);
      }
      else {
        rangeVisuals.gameObject.SetActive(false);
      }
    }
  }
}
