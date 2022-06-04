using System;
using UnityEngine;

namespace bts {
  public class UnitStats : ScriptableObject {
    public event Action OnHealthUpgrade = delegate { };
    [SerializeField] float maxHealth;
    public float MaxHealth {
      get => maxHealth;
      set {
        maxHealth = value;
        OnHealthUpgrade.Invoke();
      }
    }
    
    public event Action OnSpeedUpgrade = delegate { };
    [SerializeField] float movementSpeed;
    public float MovementSpeed { 
      get => movementSpeed;
      set {
        movementSpeed = value;
        OnSpeedUpgrade.Invoke();
      }
    }

    public float damageAmount;
    public float timeBetweenAttacks;
    public float timeBetweenGathers;
    public GemstoneDictionary gatherBonuses;
    
    void OnEnable() {
      gatherBonuses = new GemstoneDictionary();
      foreach (GemstoneType type in Enum.GetValues(typeof(GemstoneType))) {
        gatherBonuses[type] = 0;
      }
    }
  }
}