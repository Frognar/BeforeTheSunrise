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
    GridXZ<GridBuildingSystem.GridObject> grid;
    GridBuildingSystem buildingSystem;
    PlacedObjectType objectType;

    void OnEnable() {
      inputReader.WorldPositionEvent += UpdatePosition;
    }

    void OnDisable() {
      inputReader.WorldPositionEvent -= UpdatePosition;
    }

    void UpdatePosition(Vector3 worldPosition) {
      Vector3Int cords = grid.GetCords(worldPosition);
      transform.position = grid.GetWorldPosition(cords);
      canBuild.value = buildingSystem.CanBuild(cords, objectType);
      UpdateMaterial();
    }
    
    void UpdateMaterial() {
      meshRenderer.material = canBuild ? canPlaceMaterial : cantPlaceMaterial;
    }

    public void SetUp(PlacedObjectType buildingType) {
      objectType = buildingType;
      boxCollider.size = new Vector3(buildingType.width - .1f, .5f, buildingType.height - .1f);
      CustomBuildingData buildingData = buildingType.customData as CustomBuildingData;
      meshFilter.mesh = buildingData.ghostMesh;
      meshObject.transform.localPosition = new Vector3(buildingType.width / 2f, 0, buildingType.height / 2f);
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
    
    void Awake() {
      buildingSystem = FindObjectOfType<GridBuildingSystem>();
    }

    void Start() {
      grid = buildingSystem.Grid;
    }
  }
}
