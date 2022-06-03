using System.Collections.Generic;
using UnityEngine;

namespace bts {
  public class Gemstone : PlacedObject {
    [SerializeField] List<Mesh> avaliableMeshes;
    [field: SerializeField] public GemstoneType GemstoneType { get; private set; }
    public override string Name => PlaceableObjectType.name;
    [field: SerializeField] public int BaseGatherAmount { get; private set; } = 1;

    protected override void Start() {
      base.Start();
      GetComponentInChildren<MeshFilter>().mesh = avaliableMeshes[Random.Range(0, avaliableMeshes.Count)];
      bool isInfused = Random.value < 0.1f;
      if (isInfused) {
        BaseGatherAmount += 1;
        GetComponentInChildren<ParticleSystem>().Play();
      }
    }

    public override Dictionary<DataType, object> GetData() {
      return new Dictionary<DataType, object>() {
        { DataType.Name, Name },
        { DataType.GemstoneType, GemstoneType },
        { DataType.GatherAmount, BaseGatherAmount }
      };
    }
  }
}
