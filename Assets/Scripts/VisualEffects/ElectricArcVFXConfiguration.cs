using UnityEngine;
using UnityEngine.VFX;

namespace bts {
  [CreateAssetMenu(fileName = "Electric Arc Configuration", menuName = "VFX/Electric Arc Configuration")]
  public class ElectricArcVFXConfiguration : ScriptableObject {
    [field: SerializeField] public float SourceTangetLength { get; private set; }
    [field: SerializeField] public float TargetTangetLength { get; private set; }
    [field: SerializeField] public Vector3Asset Color { get; private set; }
    [field: SerializeField] public float Duration { get; private set; }

    public void ApplyTo(VisualEffect electricArc) {
      electricArc.SetFloat("SourceTangentLength", SourceTangetLength);
      electricArc.SetFloat("TargetTangentLength", TargetTangetLength);
      electricArc.SetVector3("ArcColor", Color);
    }
  }
}
