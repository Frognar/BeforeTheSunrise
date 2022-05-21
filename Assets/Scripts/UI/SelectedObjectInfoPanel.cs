using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace bts {
  public class SelectedObjectInfoPanel : MonoBehaviour {
    [SerializeField] TMP_Text objectNameText;

    public void UpdateUI(List<Selectable> selected) {
      objectNameText.text = selected.First().Name;
    }
  }
}
