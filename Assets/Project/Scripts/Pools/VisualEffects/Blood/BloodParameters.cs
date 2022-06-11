using fro.Pools;
using UnityEngine;

namespace bts {
  public class BloodParameters : PooledObjectParameters<BloodVFX> {
    public Vector3 Position { get; set; }

    public override void SetTo(BloodVFX vfx) {
      vfx.transform.position = Position;
    }
  }
}
