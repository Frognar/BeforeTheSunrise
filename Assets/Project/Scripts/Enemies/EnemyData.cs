using UnityEngine;

namespace bts {
  [CreateAssetMenu(fileName = "Enemy Data", menuName = "Enemies/Enemy Data")]
  public class EnemyData : ScriptableObject {
    [field: SerializeField] public float MaxHealth { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public float TimeBetweenAttacks { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
  }
}
