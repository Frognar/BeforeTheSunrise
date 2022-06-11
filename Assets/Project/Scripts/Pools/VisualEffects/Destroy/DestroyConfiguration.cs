using fro.Pools;
using UnityEngine;

namespace bts {
  [CreateAssetMenu(menuName = "VFX/Destroy Configuration", fileName = "Destroy Configuration")]
  public class DestroyConfiguration : PooledObjectConfiguration<DestroyVFX> {
    [SerializeField][ColorUsage(showAlpha: true, hdr: true)] Color color;
    [SerializeField] float duration;

    public override void ApplyTo(DestroyVFX vfx) {
      vfx.VisualEffect.SetVector4("Color", color);
      vfx.ReleaseAfter(duration);
    }
  }
}
