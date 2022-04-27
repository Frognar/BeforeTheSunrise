using System;

namespace bts {
  public class HealthAnimated : Health {
    Health Health { get; }
    bool InstantDecrease { get; }
    int ChangeAmount { get; }
    bool IsAnimating { get; set; }

    public HealthAnimated(Health health, bool instantDecrease = true, int changeAmount = 1) : base(health.MaxHealth) {
      Health = health;
      InstantDecrease = instantDecrease;
      ChangeAmount = changeAmount;
      Health.OnValueChange += OnHealthChange;
      TimeTicker.OnTick += UpdateOnTick;
    }

    ~HealthAnimated() {
      Health.OnValueChange -= OnHealthChange;
      TimeTicker.OnTick -= UpdateOnTick;
    }

    void OnHealthChange(object sender, EventArgs e) {
      IsAnimating = true;
    }

    void UpdateOnTick(object sender, EventArgs e) {
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
