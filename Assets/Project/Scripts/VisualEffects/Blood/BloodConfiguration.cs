using UnityEngine;

namespace bts {
  [CreateAssetMenu(menuName = "VFX/Blood Configuration", fileName = "Blood Configuration")]
  public class BloodConfiguration : VFXConfiguration<BloodVFX> {
    [SerializeField][ColorUsage(showAlpha: true, hdr: true)] Color color;
    [SerializeField] float duration;

    public override void ApplyTo(BloodVFX vfx) {
      vfx.VisualEffect.SetVector4("Color", color);
      vfx.ReleaseAfter(duration);
    }
  }
}
