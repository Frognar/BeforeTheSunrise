using UnityEngine;

namespace fro.HealthSystem {
  public class HealthComponent : MonoBehaviour {
    [SerializeField] WorldHealthBar healthBar;
    public Health Health { get; private set; }

    public void Init(float maxHealth) {
      Health = new Health(maxHealth);
      healthBar.SetUp(Health);
    }

    public void Damage(float amount) {
      if (Health != null) {
        Health.Damage(amount);
      }
    }

    public void Heal(float amount) {
      if (Health != null) {
        Health.Heal(amount);
      }
    }

    public float GetMaxHealth() {
      if (Health != null) {
        return Health.MaxHealth;
      }
      
      return 0;
    }
    
    public float GetCurrentHealth() {
      if (Health != null) {
        return Health.CurrentHealth;
      }

      return 0;
    }
  }
}
