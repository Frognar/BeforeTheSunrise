using UnityEngine;

namespace bts {
  [CreateAssetMenu(menuName = "VFX/Blood Configuration", fileName = "Blood Configuration")]
  public class BloodConfiguration : ScriptableObject {
    [field: SerializeField][field: ColorUsage(showAlpha: true, hdr: true)]public Color Color { get; private set; }
  }
}
