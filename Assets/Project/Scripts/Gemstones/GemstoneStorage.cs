using System;
using System.Collections.Generic;
using UnityEngine;

namespace bts.Gemstones {
  [CreateAssetMenu()]
  public class GemstoneStorage : ScriptableObject {
    public event EventHandler StorageChanged;
    [field: SerializeField] public GemstoneDictionary Gemstones { get; private set; }

    public void Reset() {
      foreach (GemstoneType type in Enum.GetValues(typeof(GemstoneType))) {
#if UNITY_EDITOR
        Gemstones[type] = 100;
#else
        Gemstones[type] = 0;
#endif
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

    public void Refund(IDictionary<GemstoneType, int> costs, float refundRate) {
      foreach (KeyValuePair<GemstoneType, int> gemstone in costs) {
        Gemstones[gemstone.Key] += Mathf.FloorToInt(gemstone.Value * refundRate);
      }

      StorageChanged?.Invoke(this, EventArgs.Empty);
    }

    public GemstoneType GetRandomType() {
      Array values = Enum.GetValues(typeof(GemstoneType));
      GemstoneType type = (GemstoneType)values.GetValue(UnityEngine.Random.Range(0, values.Length));
      return type;
    }

    public void StoreRandom(int amount) {
      Array values = Enum.GetValues(typeof(GemstoneType));
      GemstoneType type = (GemstoneType)values.GetValue(UnityEngine.Random.Range(0, values.Length));
      Store(type, amount);
    }
  }
}
