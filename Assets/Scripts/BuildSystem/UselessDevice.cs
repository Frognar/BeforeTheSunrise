using System;
using UnityEngine;

namespace bts {
  public class UselessDevice : PlacedObject, ElectricDevice {
    [SerializeField] float energyPerAttack;
    [SerializeField] ParticleSystem particles;
    [field: SerializeField] public float MaxEnergy { get; private set; }
    [field: SerializeField] float ElectricDevice.CurrentEnergy { get; set; }

    void OnEnable() {
      TimeTicker.OnTick += Attack;
    }

    void OnDisable() {
      TimeTicker.OnTick -= Attack;
    }

    void Attack(object s, EventArgs e) {
      if ((this as ElectricDevice).CanAfford(energyPerAttack)) {
        (this as ElectricDevice).Use(energyPerAttack);
        particles.Play();
      }
    }
  }
}
