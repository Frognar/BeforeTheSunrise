using UnityEngine;

namespace bts {
  public class BloodParameters : VFXParameters<BloodVFX> {
    public Vector3 Position { get; set; }

    public override void SetTo(BloodVFX vfx) {
      vfx.transform.position = Position;
    }
  }
}
