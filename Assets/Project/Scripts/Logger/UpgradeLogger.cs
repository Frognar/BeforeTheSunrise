using System.Collections.Generic;
using bts.Gemstones;
using UnityEngine;

namespace bts {
  public class UpgradeLogger : MonoBehaviour, Loggable {
    [SerializeField] UnitStats stats;
    
    public string GetLogData() {
      return string.Join('\n', GetStatsLogData());
    }
    
    IEnumerable<string> GetStatsLogData() {
      yield return "Stats:";
      yield return $"\tMax Health: {stats.MaxHealth}";
      yield return $"\tMovement Speed: {stats.MovementSpeed}";
      yield return $"\tDamage: {stats.damageAmount}";
      yield return $"\tTime Between Attacks: {stats.timeBetweenAttacks}";
      yield return $"\tTime Between Gathers: {stats.timeBetweenGathers}";
      yield return $"\tGather Upgrgades:";
      foreach (GemstoneType gemstone in stats.gatherBonuses.Keys) {
        yield return $"\t\t{gemstone}: {stats.gatherBonuses[gemstone]}";
      }
    }
  }
}
