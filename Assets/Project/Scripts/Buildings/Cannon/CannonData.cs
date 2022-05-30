﻿using UnityEngine;

namespace bts {
  [CreateAssetMenu(menuName = "Buildings/Data/Cannon", fileName = "CannonData")]
  public class CannonData : CustomBuildingData {
    public float range;
    public float damage;
    public float energyPerAttack;
    public float maxEnergy;
    public float timeBetweenAttacks;
    public ElectricArcVFXConfiguration electricArcConfig;
    public AudioClipsGroup attackSFX;
    public AudioConfiguration audioConfig;
  }
}