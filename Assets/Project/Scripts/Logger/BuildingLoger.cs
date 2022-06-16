using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace bts {
  public class BuildingLoger : MonoBehaviour, Loggable {
    [SerializeField] BuildingRegister buildingRegister;
    
    public string GetLogData() {
      return string.Join('\n', GetBuildingLogData());
    }

    IEnumerable<string> GetBuildingLogData() {
      yield return "Buildings:";
      foreach (string buildingData in buildingRegister.Buildings.Select(b => b.GetLogData())) {
        yield return $"\t{buildingData}";
      }
    }
  }
}
