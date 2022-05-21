namespace bts {
  public interface ElectricDevice {
    float MaxEnergy { get; }
    float CurrentEnergy { get; }
    float NormalizedEnergy => CurrentEnergy / MaxEnergy;
    bool IsFull => CurrentEnergy == MaxEnergy;
    
    bool CanAfford(float energy) {
      return CurrentEnergy >= energy;
    }

    void Use(float energy);

    void Store(float energy);
  }
}
