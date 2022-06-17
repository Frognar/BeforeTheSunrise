using System.Collections.Generic;
using UnityEngine;

namespace bts {
  public class SelectionSaver : MonoBehaviour {
    [SerializeField] InputReader inputReader;
    [SerializeField] Selector selector;
    Dictionary<int, List<Selectable>> selectionGroups;

    void Awake() {
      selectionGroups = new Dictionary<int, List<Selectable>>();
    }

    void OnEnable() {
      inputReader.GroupSelectionEvent += OnSelectionGroup;
    }

    void OnDisable() {
      inputReader.GroupSelectionEvent -= OnSelectionGroup;
    }

    void OnSelectionGroup(object sender, InputReader.GroupSelectionEventArgs e) {
      if (e.ShouldSave) {
        selectionGroups[e.GroupId] = new List<Selectable>(selector.Selected);
      }
      else if (selectionGroups.ContainsKey(e.GroupId)) {
        FilterDestroyedSelectables(e.GroupId);
        if (selectionGroups[e.GroupId].Count > 0) {
          selector.Select(new List<Selectable>(selectionGroups[e.GroupId]));
        }
      }
    }
    
    void FilterDestroyedSelectables(int groupId) {
        selectionGroups[groupId] = selectionGroups[groupId].FindAll(s => (s as Object) != null);
    }
  }
}
