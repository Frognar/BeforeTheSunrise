namespace bts {
  public class HealthAnimated : Health {
    Health Health { get; }
    bool InstantDecrease { get; }
    float ChangeAmount { get; }
    bool IsAnimating { get; set; }

    public HealthAnimated(Health health, bool instantDecrease = true, float changeAmount = 1)
      : base(health.MaxHealth) {
      Health = health;
      InstantDecrease = instantDecrease;
      ChangeAmount = changeAmount;
      Health.OnValueChange += OnHealthChange;
      Health.OnReset += OnHealthReset;
      Health.OnUpgrade += OnHealthUpgrade;
    }

    private void OnHealthUpgrade() {
      MaxHealth = Health.MaxHealth;
      CurrentHealth = Health.CurrentHealth;
    }

    ~HealthAnimated() {
      Health.OnValueChange -= OnHealthChange;
      Health.OnReset -= OnHealthReset;
      Health.OnUpgrade -= OnHealthUpgrade;
    }

    void OnHealthChange() {
      IsAnimating = true;
    }

    void OnHealthReset() {
      IsAnimating = false;
      Heal(MaxHealth);
    }

    public void Update() {
      UpdateValue();
    }

    void UpdateValue() {
      if (IsAnimating) {
        if (CurrentHealth > Health.CurrentHealth) {
          if (InstantDecrease) {
            Damage(CurrentHealth - Health.CurrentHealth);
          }
          else {
            Damage(ChangeAmount);
          }
        }
        else if (CurrentHealth < Health.CurrentHealth) {
          Heal(ChangeAmount);
        }
        else {
          IsAnimating = false;
        }
      }
    }
  }
}
