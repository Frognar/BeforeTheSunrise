using System;
using System.Collections.Generic;
using RotaryHeart.Lib.SerializableDictionary;

namespace bts.Gemstones {
  [Serializable]
  public class GemstoneDictionary : SerializableDictionaryBase<GemstoneType, int> {
    public static GemstoneDictionary operator *(GemstoneDictionary gemstoneDictionary, int multiplier) {
      GemstoneDictionary newGemstoneDictionary = new GemstoneDictionary();
      foreach (KeyValuePair<GemstoneType, int> keyValuePair in gemstoneDictionary) {
        newGemstoneDictionary.Add(keyValuePair.Key, keyValuePair.Value * multiplier);
      }

      return newGemstoneDictionary;
    }

    public void Add(GemstoneDictionary gemstones) {
      foreach (KeyValuePair<GemstoneType, int> keyValuePair in gemstones) {
        if (ContainsKey(keyValuePair.Key)) {
          this[keyValuePair.Key] += keyValuePair.Value;
        }
        else {
          Add(keyValuePair.Key, keyValuePair.Value);
        }
      }
    }
  }
}
