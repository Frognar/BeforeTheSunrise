using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace bts {
  public class BuildingRegister : ScriptableObject {
    public int CurrentTechnologyLevel => Bases.Any() ? Bases.Max(b => b.BuildingLevel) : 0;
    IEnumerable<Building> Bases => buildings.Where(b => b is Base);
    List<Building> buildings;

    void OnEnable() {
      buildings = new List<Building>();
    }

    public void Register(Building building) {
      buildings.Add(building);
    }

    public void Unregister(Building building) {
      _ = buildings.Remove(building);
    }
  }
}
