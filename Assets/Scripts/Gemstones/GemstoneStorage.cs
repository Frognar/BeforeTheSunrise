using System;
using System.Collections.Generic;
using UnityEngine;

namespace bts {
  [CreateAssetMenu()]
  public class GemstoneStorage : ScriptableObject {
    public event EventHandler StorageChanged;
    public Dictionary<GemstoneType, int> Gemstones { get; private set; }

    void OnEnable() {
      Gemstones = new Dictionary<GemstoneType, int>();
      foreach (GemstoneType type in Enum.GetValues(typeof(GemstoneType))) {
        Gemstones[type] = 0;
      }
    }

    public bool CanAfford(Dictionary<GemstoneType, int> gemstones) {
      foreach (KeyValuePair<GemstoneType, int> gemstone in gemstones) {
        if (!CanAfford(gemstone.Key, gemstone.Value)) {
          return false;
        }
      }

      return true;
    }

    public bool CanAfford(GemstoneType type, int amount) {
      return Gemstones[type] >= amount;
    }

    public void Store(Dictionary<GemstoneType, int> gemstones) {
      foreach (KeyValuePair<GemstoneType, int> gemstone in gemstones) {
        Gemstones[gemstone.Key] += gemstone.Value;
      }

      StorageChanged?.Invoke(this, EventArgs.Empty);
    }
    
    public void Store(GemstoneType type, int amount) {
      Gemstones[type] += amount;
      StorageChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Discard(Dictionary<GemstoneType, int> gemstones) {
      foreach (KeyValuePair<GemstoneType, int> gemstone in gemstones) {
        Gemstones[gemstone.Key] -= gemstone.Value;
      }

      StorageChanged?.Invoke(this, EventArgs.Empty);
    }
   
    public void Discard(GemstoneType type, int amount) {
      Gemstones[type] -= amount;
      StorageChanged?.Invoke(this, EventArgs.Empty);
    }
  }
}
