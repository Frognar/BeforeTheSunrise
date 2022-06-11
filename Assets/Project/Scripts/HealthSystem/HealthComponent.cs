using UnityEngine;

namespace fro.HealthSystem {
  public class HealthComponent : MonoBehaviour {
    [SerializeField] WorldHealthBar healthBar;
    public Health Health { get; private set; }

    public void Init(float maxHealth) {
      Health = new Health(maxHealth);
      healthBar.SetUp(Health);
    }
  }
}
