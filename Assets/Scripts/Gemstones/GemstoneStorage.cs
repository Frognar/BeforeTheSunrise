using System;
using System.Collections.Generic;
using UnityEngine;

namespace bts {
  [CreateAssetMenu()]
  public class GemstoneStorage : ScriptableObject {
    public event EventHandler StorageChanged;
    [SerializeField] GemstoneDictionary gemstones;
    public GemstoneDictionary Gemstones => gemstones;

    void OnEnable() {
      foreach (GemstoneType type in Enum.GetValues(typeof(GemstoneType))) {
        Gemstones[type] = 0;
      }
    }

    public bool CanAfford(IDictionary<GemstoneType, int> gemstones) {
      foreach (KeyValuePair<GemstoneType, int> gemstone in gemstones) {
        if (Gemstones[gemstone.Key] < gemstone.Value) {
          return false;
        }
      }

      return true;
    }

    public void Store(IDictionary<GemstoneType, int> gemstones) {
      foreach (KeyValuePair<GemstoneType, int> gemstone in gemstones) {
        Gemstones[gemstone.Key] += gemstone.Value;
      }

      StorageChanged?.Invoke(this, EventArgs.Empty);
    }
    
    public void Store(GemstoneType type, int amount) {
      Gemstones[type] += amount;
      StorageChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Discard(IDictionary<GemstoneType, int> gemstones) {
      foreach (KeyValuePair<GemstoneType, int> gemstone in gemstones) {
        Gemstones[gemstone.Key] -= gemstone.Value;
      }

      StorageChanged?.Invoke(this, EventArgs.Empty);
    }
  }
}
