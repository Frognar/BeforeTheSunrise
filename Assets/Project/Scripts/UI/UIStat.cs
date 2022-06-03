using TMPro;
using UnityEngine;

namespace bts {
  public class UIStat : MonoBehaviour {
    [SerializeField] TMP_Text statName;
    [SerializeField] TMP_Text statValue;

    public void UpdateStat(string name, object value) {
      statName.text = name;
      statValue.text = value.ToString();
    }
  }
}
