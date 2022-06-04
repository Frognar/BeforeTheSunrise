﻿using UnityEngine;

namespace bts {
  public abstract class VFXConfiguration<T> : ScriptableObject
    where T : MonoBehaviour, Poolable {
    public abstract void ApplyTo(T vfx);
  }
}