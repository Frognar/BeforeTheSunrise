using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace bts {
  public class SelectedObjectInfoPanel : MonoBehaviour {
    [SerializeField] TMP_Text objectNameText;

    public void UpdateUI(List<Selectable> selected) {
      if (selected.Count == 1) {
        objectNameText.text = selected.First().Name;
      }
      else {
        objectNameText.text = $"{selected.First().Name}: {selected.Count}";
      }
    }
  }
}
