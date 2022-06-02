﻿using UnityEngine;

namespace bts {
  [CreateAssetMenu(menuName = "VFX/Laser Configuration", fileName = "Laser Configuration")]
  public class LaserConfiguration : VFXConfiguration<LaserVFX> {
    [SerializeField] Material material;

    public override void ApplyTo(LaserVFX vfx) {
      vfx.LineRenderer.material = material;
    }
  }
}
