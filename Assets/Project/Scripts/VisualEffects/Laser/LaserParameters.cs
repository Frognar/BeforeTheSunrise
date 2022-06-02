using UnityEngine;

namespace bts {
  public class LaserParameters : VFXParameters<LaserVFX> {
    public Vector3 SourcePosition { get; set; }
    public Transform Target { get; set; }

    public override void SetTo(LaserVFX vfx) {
      vfx.transform.position = SourcePosition;
      vfx.Target = Target;
      vfx.LineRenderer.SetPositions(new Vector3[] { SourcePosition, Target.position });
    }
  }
}
