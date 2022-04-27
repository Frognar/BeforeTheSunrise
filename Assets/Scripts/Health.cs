using System;

namespace bts {
  public class Health {
    public event EventHandler OnValueChange;
    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; protected set; }
    public float HealthNormalized => (float)CurrentHealth / MaxHealth;

    public Health(int maxHealth) {
      MaxHealth = maxHealth;
      CurrentHealth = maxHealth;
    }

    public virtual void Damage(int amount) {
      CurrentHealth -= amount;
      if (CurrentHealth < 0) {
        CurrentHealth = 0;
      }

      OnValueChange?.Invoke(this, EventArgs.Empty);
    }

    public virtual void Heal(int amount) {
      CurrentHealth += amount;
      if (CurrentHealth > MaxHealth) {
        CurrentHealth = MaxHealth;
      }

      OnValueChange?.Invoke(this, EventArgs.Empty);
    }
  }
}
