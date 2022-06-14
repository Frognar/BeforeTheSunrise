using System.Collections.Generic;
using bts.Gemstones;
using UnityEngine;

namespace bts {
  public class Gemstone : MonoBehaviour, Selectable {
    public event System.Action<Dictionary<DataType, object>> OnDataChange = delegate { };
    [Header("Visuals")]
    [SerializeField] MeshFilter meshFilter;
    [SerializeField] List<Mesh> avaliableMeshes;
    [SerializeField] ParticleSystem particles;

    [field: Header("Gemstone Data")]
    [field: SerializeField] public GemstoneType GemstoneType { get; private set; }
    [field: SerializeField] public int BaseGatherAmount { get; private set; } = 1;

    public string Name => nameof(Gemstone);
    public Affiliation ObjectAffiliation => Affiliation.Neutral;
    public Type ObjectType => Type.Resources;
    [field: SerializeField] public Transform Center { get; private set; }
    [field: SerializeField] public GameObject Selected { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public SelectablesEventChannel SelectablesEventChannel { get; private set; }

    void Start() {
      meshFilter.mesh = avaliableMeshes[Random.Range(0, avaliableMeshes.Count)];
      bool isInfused = Random.value < 0.1f;
      if (isInfused) {
        BaseGatherAmount += 1;
        particles .Play();
      }
    }

    public Dictionary<DataType, object> GetData() {
      return new Dictionary<DataType, object>() {
        { DataType.Name, Name },
        { DataType.GemstoneType, GemstoneType },
        { DataType.GatherAmount, BaseGatherAmount }
      };
    }

    public void Select() {
      Selected.SetActive(true);
    }

    public void Deselect() {
      Selected.SetActive(false);
    }

    public bool IsSameAs(Selectable other) {
      return false;
    }
  }
}
