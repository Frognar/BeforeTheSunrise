using UnityEngine;

namespace bts {
  [CreateAssetMenu(menuName = "VFX/Electric Arc Configuration", fileName = "Electric Arc Configuration")]
  public class ElectricArcVFXConfiguration : VFXConfiguration<ElectricArcVFX> {
    [SerializeField] float sourceTangetLength;
    [SerializeField] float targetTangetLength;
    [SerializeField][ColorUsage(showAlpha: true, hdr: true)] Color color;
    [SerializeField] float duration;

    public override void ApplyTo(ElectricArcVFX vfx) {
      vfx.VisualEffect.SetFloat("SourceTangentLength", sourceTangetLength);
      vfx.VisualEffect.SetFloat("TargetTangentLength", targetTangetLength);
      vfx.VisualEffect.SetVector4("ArcColor", color);
      vfx.ReleaseAfter(duration);
    }
  }
}
