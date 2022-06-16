using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace bts {
  public class BuildingRegister : ScriptableObject {
    public int CurrentTechnologyLevel => Bases.Any() ? Bases.Max(b => b.BuildingLevel) : 0;
    IEnumerable<Building> Bases => Buildings.Where(b => b is Base);
    public List<Building> Buildings { get; private set; }

    void OnEnable() {
      Buildings = new List<Building>();
    }

    public void Register(Building building) {
      Buildings.Add(building);
    }

    public void Unregister(Building building) {
      _ = Buildings.Remove(building);
    }
  }
}
