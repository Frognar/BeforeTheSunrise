using System.Collections.Generic;
using bts.Gemstones;
using UnityEngine;

namespace bts {
  public class StorageLogger : MonoBehaviour, Loggable {
    [SerializeField] GemstoneStorage storage;
    
    public string GetLogData() {
      return string.Join('\n', GetStorageLogData());
    }

    IEnumerable<string> GetStorageLogData() {
      yield return "Gemstones:";
      foreach (GemstoneType gemstone in storage.Gemstones.Keys) {
        yield return $"\t{gemstone}: {storage.Gemstones[gemstone]}";
      }
    }
  }
}
