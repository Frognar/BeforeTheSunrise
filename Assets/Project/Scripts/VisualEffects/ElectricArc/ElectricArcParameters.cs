using UnityEngine;

namespace bts {
  public class ElectricArcParameters : VFXParameters<ElectricArcVFX> {
    public Transform Source { get; set; }
    public Vector3 TargetPosition { get; set; }

    public override void SetTo(ElectricArcVFX vfx) {
      vfx.transform.position = Source.position;
      vfx.SourceTransformBinder.Target = Source;
      vfx.VisualEffect.SetVector3("TargetPosition", TargetPosition);
    }
  }
}
