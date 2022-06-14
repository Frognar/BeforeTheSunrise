using bts.Gemstones;
using fro.Pools;
using UnityEngine;

namespace bts {
  public class PopupTextParameters : PooledObjectParameters<PopupText> {
    public Vector3 Position { get; set; }
    public GemstoneType GemstoneType { get; set; }
    public string Text { get; set; }

    public override void SetTo(PopupText obj) {
      obj.transform.position = Position + Vector3.up;
      obj.SetGemstone(GemstoneType);
      obj.Text.text = Text;
    }
  }
}
