namespace bts {
  public interface ElectricDevice {
    float MaxEnergy { get; }
    float CurrentEnergy { get; }
    float NormalizedEnergy => CurrentEnergy / MaxEnergy;
    bool IsFull => CurrentEnergy == MaxEnergy;

    bool CanAfford(float energy);

    void UseEnergy(float energy);

    void StoreEnergy(float energy);
  }
}
