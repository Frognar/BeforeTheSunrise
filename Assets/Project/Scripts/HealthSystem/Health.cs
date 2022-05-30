using System;

namespace bts {
  public class Health {
    public event Action OnReset = delegate { };
    public event Action OnValueChange = delegate { };
    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; protected set; }
    public float HealthNormalized => CurrentHealth / MaxHealth;
    public bool HasFullHealth => CurrentHealth == MaxHealth;
    public bool HasNoHealth => CurrentHealth == 0;

    public Health(float maxHealth) {
      MaxHealth = maxHealth;
      CurrentHealth = maxHealth;
    }

    public virtual void Damage(float amount) {
      CurrentHealth -= amount;
      if (CurrentHealth < 0) {
        CurrentHealth = 0;
      }

      OnValueChange.Invoke();
    }

    public virtual void Heal(float amount) {
      CurrentHealth += amount;
      if (CurrentHealth > MaxHealth) {
        CurrentHealth = MaxHealth;
      }

      OnValueChange.Invoke();
    }

    public virtual void Reset() {
      CurrentHealth = MaxHealth;
      OnReset.Invoke();
    }
  }
}
