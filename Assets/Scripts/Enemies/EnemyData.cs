using UnityEngine;

namespace bts {
  [CreateAssetMenu(fileName = "Enemy Data", menuName = "Enemies/Enemy Data")]
  public class EnemyData : ScriptableObject {
    [field: SerializeField] public int MaxHealth { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float TimeBetweenAttacks { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
  }
}
