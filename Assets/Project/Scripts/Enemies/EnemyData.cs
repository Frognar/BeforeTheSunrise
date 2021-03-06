using UnityEngine;

namespace bts {
  [CreateAssetMenu(fileName = "Enemy Data", menuName = "Enemies/Enemy Data")]
  public class EnemyData : ScriptableObject {
    [field: SerializeField] public float MaxHealth { get; set; }
    [field: SerializeField] public float Damage { get; set; }
    [field: SerializeField] public float TimeBetweenAttacks { get; private set; }
    public float DamagePerSecond => Damage / TimeBetweenAttacks;
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public AudioConfiguration AudioConfig { get; private set; }
    [field: SerializeField] public AudioClipsGroup EnemyDeathSFX { get; private set; }
    [field: SerializeField] public AudioClipsGroup EnemyAttackSFX { get; private set; }
    [field: SerializeField] public AudioClipsGroup EnemySelectSFX { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
  }
}
