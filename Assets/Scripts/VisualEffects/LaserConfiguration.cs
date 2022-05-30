using UnityEngine;

namespace bts {
  [CreateAssetMenu(menuName = "VFX/Laser Configuration", fileName = "Laser Configuration")]
  public class LaserConfiguration : ScriptableObject {
    [field: SerializeField] public Material Material { get; private set; }
  }
}
