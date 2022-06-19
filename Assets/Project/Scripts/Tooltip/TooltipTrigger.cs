using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace bts {
  public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] TooltipSystem tooltipSystem;
    [SerializeField] TooltipData tooltipData;
    Coroutine showTooltipCoroutine;
    bool entered;
    bool isEnabled;

    public void SetUp(TooltipData data) {
      tooltipData = data;
      if (isEnabled) {
        tooltipSystem.Show(tooltipData);
      }
    }

    public void OnPointerEnter(PointerEventData eventData) {
      showTooltipCoroutine = StartCoroutine(ShowTooltip());
      entered = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
      HideTooltip();
    }

    void HideTooltip() {
      entered = false;
      tooltipSystem.Hide();
      isEnabled = false;
      if (showTooltipCoroutine != null) {
        StopCoroutine(showTooltipCoroutine);
        showTooltipCoroutine = null;
      }
    }

    IEnumerator ShowTooltip() {
      yield return new WaitForSeconds(0.5f);
      tooltipSystem.Show(tooltipData);
      isEnabled = true;
    }

    void OnDisable() {
      if (entered) {
        HideTooltip();
      }
    }
  }
}
