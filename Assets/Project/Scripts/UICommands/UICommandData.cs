using UnityEngine;

namespace bts {
  public abstract class UICommandData : ScriptableObject {
    [field: SerializeField] public Sprite ButtonIcon { get; private set; }
    public abstract TooltipData TooltipData { get; }
  }
}
