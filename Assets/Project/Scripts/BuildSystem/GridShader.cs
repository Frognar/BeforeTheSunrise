using UnityEngine;

namespace bts {
  public class GridShader : MonoBehaviour {
    [SerializeField] InputReader inputReader;
    [SerializeField] BoolAsset showGraph;
    Material groundMaterial;
    bool lastShowGraph;

    void Awake() {
      groundMaterial = GetComponent<MeshRenderer>().material;
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
