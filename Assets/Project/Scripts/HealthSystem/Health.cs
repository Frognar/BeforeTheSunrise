using System;

namespace fro.HealthSystem {
  public class Health {
    public event EventHandler OnCurrentHealthChange = delegate { };
    public event EventHandler OnMaxHealthChange = delegate { };
    public event EventHandler OnDamage = delegate { };
    public event EventHandler OnHeal = delegate { };
    public event EventHandler OnDie = delegate { };
    public event EventHandler OnFullHealth = delegate { };
    public float MaxHealth { get; protected set; }
    public float CurrentHealth { get; protected set; }
    public float HealthNormalized => CurrentHealth / MaxHealth;
    public bool IsInFullHealth => CurrentHealth >= MaxHealth;
    public bool IsDead => CurrentHealth <= 0;

    public Health(float maxHealth) {
      MaxHealth = maxHealth;
      CurrentHealth = maxHealth;
    }

    public virtual void Damage(float amount) {
      CurrentHealth -= amount;
      if (IsDead) {
        CurrentHealth = 0;
      }

      OnDamage.Invoke(this, EventArgs.Empty);
      OnCurrentHealthChange.Invoke(this, EventArgs.Empty);
      if (IsDead) {
        OnDie.Invoke(this, EventArgs.Empty);
      }
    }

    public virtual void Heal(float amount) {
      CurrentHealth += amount;
      if (IsInFullHealth) {
        CurrentHealth = MaxHealth;
      }

      OnHeal.Invoke(this, EventArgs.Empty);
      OnCurrentHealthChange.Invoke(this, EventArgs.Empty);
      if (IsInFullHealth) {
        OnFullHealth.Invoke(this, EventArgs.Empty);
      }
    }

    public virtual void Reset() {
      CurrentHealth = MaxHealth;
      OnCurrentHealthChange.Invoke(this, EventArgs.Empty);
    }

    public void ChangeMaxHealth(float maxHealth) {
      float scale = HealthNormalized;
      MaxHealth = maxHealth;
      CurrentHealth = MaxHealth * scale;
      OnMaxHealthChange.Invoke(this, EventArgs.Empty);
      OnCurrentHealthChange.Invoke(this, EventArgs.Empty);
    }
  }
}
