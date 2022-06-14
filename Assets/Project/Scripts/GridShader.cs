using fro.ValueAssets;
using UnityEngine;

namespace bts {
  public class GridShader : MonoBehaviour {
    [SerializeField] InputReader inputReader;
    [SerializeField] BoolAsset showGraph;
    [SerializeField] IntAsset worldSize;
    Material groundMaterial;
    bool lastShowGraph;

    void Awake() {
      groundMaterial = GetComponent<MeshRenderer>().material;
      groundMaterial.SetVector("_GridSize", new Vector4(worldSize.value / 2, worldSize.value / 2, 0, 0));
    }

    void Update() {
      if (lastShowGraph != showGraph) {
        lastShowGraph = showGraph;
        groundMaterial.SetInt("_ShowGrid", showGraph ? 1 : 0);
      }
      
      if (lastShowGraph) {
        groundMaterial.SetVector("_MousePosition", inputReader.WorldPosition);
      }
    }
  }
}
