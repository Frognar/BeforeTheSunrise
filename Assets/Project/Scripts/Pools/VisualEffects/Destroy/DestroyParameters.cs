using fro.Pools;
using UnityEngine;

namespace bts {
  public class DestroyParameters : PooledObjectParameters<DestroyVFX> {
    public Vector3 Position { get; }

    public DestroyParameters(Vector3 position) {
      Position = position;
    }

    public override void SetTo(DestroyVFX vfx) {
      vfx.transform.position = Position;
    }
  }
}
