namespace bts {
  public interface Damageable {
    Affiliation ObjectAffiliation { get; }
    void TakeDamage(int amount);
  }
}
