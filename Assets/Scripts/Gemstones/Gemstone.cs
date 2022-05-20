using System.Collections.Generic;
using UnityEngine;

namespace bts {
  public class Gemstone : Placeable, Selectable {
    public List<Mesh> AvaliableMeshes;
    public string Name => gemstoneType.ToString();
    public Affiliation ObjectAffiliation => Affiliation.Neutral;
    public Type ObjectType => Type.Resources;
    public Transform Transform => center;
    public Vector3 Position => center.position;
    public GameObject Selected { get; private set; }
    [SerializeField] GemstoneType gemstoneType;
    public GemstoneType GemstoneType => gemstoneType;
    public int BaseGatherAmount { get; private set; } = 1;

    void Start() {
      Selected = transform.Find("Selected").gameObject;
      float selectedScale = Mathf.Max(placedObjectType.width, placedObjectType.height) + 0.5f;
      Selected.transform.localScale = new Vector3(selectedScale, selectedScale, 1f);
      GetComponentInChildren<MeshFilter>().mesh = AvaliableMeshes[Random.Range(0, AvaliableMeshes.Count)];
      bool isInfused = Random.value < 0.1f;
      if (isInfused) {
        BaseGatherAmount = 2;
        GetComponentInChildren<ParticleSystem>().Play();
      }
    }

    public void Select() {
      Selected.SetActive(true);
    }

    public void Deselect() {
      Selected.SetActive(false);
    }
  }
}
