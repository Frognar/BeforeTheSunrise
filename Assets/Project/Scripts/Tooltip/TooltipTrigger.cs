using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace bts {
  public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] TooltipSystem tooltipSystem;
    [SerializeField] TooltipData tooltipData;
    Coroutine showTooltipCoroutine;
    bool entered;

    public void SetUp(string header, string content, GemstoneDictionary gemstones) {
      SetUp(new TooltipData(header, content, gemstones));
    }

    public void SetUp(TooltipData data) {
      tooltipData = data;
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
      if (showTooltipCoroutine != null) {
        StopCoroutine(showTooltipCoroutine);
        showTooltipCoroutine = null;
      }
    }

    IEnumerator ShowTooltip() {
      yield return new WaitForSeconds(0.5f);
      tooltipSystem.Show(tooltipData);
    }

    void OnDisable() {
      if (entered) {
        HideTooltip();
      }
    }
  }
}
