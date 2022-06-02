using UnityEngine;

namespace bts {
  public class DestroyParameters : VFXParameters<DestroyVFX> {
    public Vector3 Position { get; set; }

    public override void SetTo(DestroyVFX vfx) {
      vfx.transform.position = Position;
    }
  }
}
