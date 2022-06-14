using System;
using bts.Gemstones;
using UnityEngine;

namespace bts {
  public class UnitStats : ScriptableObject {
    public event Action OnHealthUpgrade = delegate { };
    [SerializeField] float baseMaxHealth;
    [HideInInspector] float maxHealth;
    public float MaxHealth {
      get => maxHealth;
      set {
        maxHealth = value;
        OnHealthUpgrade.Invoke();
      }
    }
    
    public event Action OnSpeedUpgrade = delegate { };
    [SerializeField] float baseMovementSpeed;
    [HideInInspector] float movementSpeed;
    public float MovementSpeed { 
      get => movementSpeed;
      set {
        movementSpeed = value;
        OnSpeedUpgrade.Invoke();
      }
    }
    
    [SerializeField] float baseDamageAmount;
    [HideInInspector] public float damageAmount;
    [SerializeField] float baseTimeBetweenAttacks;
    [HideInInspector] public float timeBetweenAttacks;
    [SerializeField] float baseTimeBetweenGathers;
    [HideInInspector] public float timeBetweenGathers;
    public GemstoneDictionary gatherBonuses;
    
    void OnEnable() {
      maxHealth = baseMaxHealth;
      movementSpeed = baseMovementSpeed;
      damageAmount = baseDamageAmount;
      timeBetweenAttacks = baseTimeBetweenAttacks;
      timeBetweenGathers = baseTimeBetweenGathers;
      ResetGatherBonus();
    }

    public void ResetGatherBonus() {
      gatherBonuses = new GemstoneDictionary();
      foreach (GemstoneType type in Enum.GetValues(typeof(GemstoneType))) {
        gatherBonuses[type] = 0;
      }
    }
  }
}