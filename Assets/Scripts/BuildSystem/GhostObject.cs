using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace bts {
  public class GhostObject : MonoBehaviour {
    [SerializeField] Material canPlaceMaterial;
    [SerializeField] Material cantPlaceMaterial;
    GridXZ<GridBuildingSystem.GridObject> grid;
    MeshRenderer[] meshRenderers;
    PlayerInputs playerInputs;
    bool canBuild;
    bool lastCanBuild;

    void Awake() {
      meshRenderers = GetComponentsInChildren<MeshRenderer>();
      playerInputs = FindObjectOfType<PlayerInputs>();
    }

    void Start() {
      grid = FindObjectOfType<GridBuildingSystem>().Grid;
    }

    void Update() {
      Vector3Int cords = grid.GetCords(playerInputs.GetMouseWorldPosition());
      transform.position = grid.GetWorldPosition(cords);

      if (lastCanBuild != canBuild) {
        lastCanBuild = canBuild;
        if (canBuild) {
          ChangeMaterial(canPlaceMaterial);
        }
        else {
          ChangeMaterial(cantPlaceMaterial);
        }
      }
    }

    void OnTriggerEnter(Collider other) {
      canBuild = false;
    }

    void OnTriggerExit(Collider other) {
      canBuild = true;
    }

    void ChangeMaterial(Material material) {
      foreach (MeshRenderer meshRenderer in meshRenderers) {
        meshRenderer.material = material;
      }
    }
  }
}
