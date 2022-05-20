namespace bts {
  public interface ElectricDevice {
    float MaxEnergy { get; }
    float CurrentEnergy { get; set; }
    float NormalizedEnergy => CurrentEnergy / MaxEnergy;
    bool IsFull => CurrentEnergy == MaxEnergy;
    
    bool CanAfford(float energy) {
      return CurrentEnergy >= energy;
    }
    
    void Use(float energy) {
      CurrentEnergy -= energy;
      if (CurrentEnergy <= 0) {
        CurrentEnergy = 0;
      }
    }

    void Store(float energy) {
      CurrentEnergy += energy;
      if (CurrentEnergy >= MaxEnergy) {
        CurrentEnergy = MaxEnergy;
      }
    }
  }
}
