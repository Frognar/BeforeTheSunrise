using UnityEngine;

namespace bts {
  public class Gemstone : Placeable, Selectable {
    public string Name => gemstoneType.ToString();
    public Affiliation ObjectAffiliation => Affiliation.Neutral;
    public Type ObjectType => Type.Resources;
    public Transform Transform => transform;
    public Vector3 Position => transform.position;
    public GameObject Selected { get; private set; }
    [SerializeField] GemstoneType gemstoneType;
    public GemstoneType GemstoneType => gemstoneType;
    public int BaseGatherAmount { get; private set; } = 1;

    void Start() {
      Selected = transform.Find("Selected").gameObject;
      bool isInfused = Random.value < 0.1f;
      if (isInfused) {
        BaseGatherAmount = 2;
        GetComponentInChildren<ParticleSystem>().Play();
      }
    }
  }
}
