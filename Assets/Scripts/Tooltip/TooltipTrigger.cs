using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace bts {
  public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    TooltipSystem tooltipSystem;
    [SerializeField] TooltipData tooltipData;
    Coroutine showTooltipCoroutine;

    public void SetUp(string header, string content, GemstoneDictionary gemstones) {
      SetUp(new TooltipData(header, content, gemstones));
    }

    public void SetUp(TooltipData data) {
      tooltipData = data;
    }

    void Awake() {
      tooltipSystem = FindObjectOfType<TooltipSystem>();
    }

    public void OnPointerEnter(PointerEventData eventData) {
      showTooltipCoroutine = StartCoroutine(ShowTooltip());
    }

    public void OnPointerExit(PointerEventData eventData) {
      StopCoroutine(showTooltipCoroutine);
      tooltipSystem.Hide();
    }

    IEnumerator ShowTooltip() {
      yield return new WaitForSeconds(0.5f);
      tooltipSystem.Show(tooltipData);
    }
  }
}
