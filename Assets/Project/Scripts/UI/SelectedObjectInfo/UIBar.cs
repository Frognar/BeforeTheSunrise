using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace bts {
  public class UIBar : MonoBehaviour {
    [SerializeField] Slider barSlider;
    [SerializeField] TMP_Text barText;

    public void UpdateBar(float current, float max) {
      barSlider.maxValue = max;
      barSlider.value = current;
      barText.text = current + "/" + max;
    }
  }
}
