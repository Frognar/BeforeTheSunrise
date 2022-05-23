using UnityEngine;

namespace bts {
  public class GhostObject : MonoBehaviour {
    [SerializeField] InputReader inputReader;
    [SerializeField] BoolAsset canBuild;
    [SerializeField] Material canPlaceMaterial;
    [SerializeField] Material cantPlaceMaterial;
    [SerializeField] MeshFilter meshFilter;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] Transform rangeVisuals;
    [SerializeField] BoxCollider boxCollider;
    GridXZ<GridBuildingSystem.GridObject> grid;
    bool lastCanBuild;

    void OnEnable() {
      inputReader.WorldPositionEvent += UpdatePosition;
    }

    void OnDisable() {
      inputReader.WorldPositionEvent -= UpdatePosition;
    }

    void UpdatePosition(Vector3 worldPosition) {
      Vector3Int cords = grid.GetCords(worldPosition);
      transform.position = grid.GetWorldPosition(cords);
      if (lastCanBuild != canBuild) {
        lastCanBuild = canBuild;
        meshRenderer.material = canBuild ? canPlaceMaterial : cantPlaceMaterial;
      }
    }

    public void SetUp(PlacedObjectTypeSO buildingType) {
      boxCollider.size = new Vector3(buildingType.width - .1f, .5f, buildingType.height - .1f);
      CustomBuildingData buildingData = buildingType.customData as CustomBuildingData;
      meshFilter.mesh = buildingData.ghostMesh;
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
      else {
        rangeVisuals.gameObject.SetActive(false);
      }
    }

    void Start() {
      grid = FindObjectOfType<GridBuildingSystem>().Grid;
    }

    void OnTriggerStay(Collider other) {
      canBuild.value = false;
    }

    void OnTriggerExit(Collider other) {
      canBuild.value = true;
    }
  }
}
