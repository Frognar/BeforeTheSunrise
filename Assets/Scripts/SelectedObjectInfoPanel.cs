using System.Linq;
using TMPro;
using UnityEngine;

namespace bts {
  public class SelectedObjectInfoPanel : MonoBehaviour {
    [SerializeField] TMP_Text objectNameText;

    public void UpdateUI(Player.OnSelectionEventArgs e) {
      objectNameText.text = e.Selected.First().Name;
    }
  }
}
