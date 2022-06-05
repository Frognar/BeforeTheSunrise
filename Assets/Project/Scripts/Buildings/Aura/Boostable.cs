namespace bts {
  public interface Boostable {
    bool IsBoosted(BoostType boostType);
    public void StartBoosting(BoostType boostType, float multiplier);
    public void StopBoosting(BoostType boostType);
  }

  public enum BoostType {
    AttackSpeed,
    Damage,
    Range
  }
}
