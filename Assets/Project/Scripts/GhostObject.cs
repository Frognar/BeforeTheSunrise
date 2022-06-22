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
      meshFilter.mesh = buildingData.ghostMesh;
      CenterMeshInLocalSpace(buildingType);
      UpdateMaterial();
      if (buildingData is Ranged rangeData) {
        ShowRangeCircle(rangeData);
      }
      else {
        HideRangeCircle();
      }
    }

    void CenterMeshInLocalSpace(PlacedObjectData buildingType) {
      meshObject.transform.localPosition = new Vector3(buildingType.Width / 2f, 0, buildingType.Height / 2f);
    }

    void ShowRangeCircle(Ranged rangeData) {
      rangeVisuals.gameObject.SetActive(true);
      float range = rangeData.Range * 2f;
      rangeVisuals.localScale = new Vector3(range, range, 1f);
    }

    void HideRangeCircle() {
      rangeVisuals.gameObject.SetActive(false);
    }
  }
}
