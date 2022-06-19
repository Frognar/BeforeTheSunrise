using System.Collections.Generic;
using UnityEngine;

namespace bts {
  public static class InRangeFinder {
    public static List<T> Find<T>(Vector3 origin, float range) {
      Collider[] collidersInRange = Physics.OverlapSphere(origin, range);
      List<T> list = new List<T>();
      foreach (Collider collider in collidersInRange) {
        if (collider.TryGetComponent(out T component) && Vector3.Distance(origin, collider.bounds.center) <= range) {
          list.Add(component);
        }
      }
      
      return list;
    }
  }
}
