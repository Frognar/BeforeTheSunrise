﻿using UnityEngine;

namespace bts {
  [CreateAssetMenu(menuName = "Buildings/Data/Generator", fileName = "GeneratorData")]
  public class GeneratorData : CustomBuildingData {
    public float range;
    public float energyPerSecond;
    public int maxDevices;
    public ElectricArcVFXConfiguration electricArcConfig;
    public AudioConfiguration audioConfig;
    public AudioClipsGroup generateSFX;
  }
}
