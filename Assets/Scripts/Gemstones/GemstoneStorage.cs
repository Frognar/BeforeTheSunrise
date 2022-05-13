using System;
using System.Collections.Generic;
using UnityEngine;

namespace bts {
  [CreateAssetMenu()]
  public class GemstoneStorage : ScriptableObject {
    Dictionary<GemstoneType, int> gemstoneStorage;

    void OnEnable() {
      gemstoneStorage = new Dictionary<GemstoneType, int>();
      foreach (GemstoneType type in Enum.GetValues(typeof(GemstoneType))) {
        gemstoneStorage[type] = 0;
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
      return gemstoneStorage[type] >= amount;
    }

    public void Store(Dictionary<GemstoneType, int> gemstones) {
      foreach (KeyValuePair<GemstoneType, int> gemstone in gemstones) {
        Store(gemstone.Key, gemstone.Value);
      }
    }
    
    public void Store(GemstoneType type, int amount) {
      gemstoneStorage[type] += amount;
    }

    public void Discard(Dictionary<GemstoneType, int> gemstones) {
      foreach (KeyValuePair<GemstoneType, int> gemstone in gemstones) {
        Discard(gemstone.Key, gemstone.Value);
      }
    }
   
    public void Discard(GemstoneType type, int amount) {
      gemstoneStorage[type] -= amount;
    }
  }
}
