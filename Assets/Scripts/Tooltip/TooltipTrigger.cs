using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace bts {
  public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    TooltipSystem tooltipSystem;
    [SerializeField] string header;
    [SerializeField] string content;
    [SerializeField] GemstoneDictionary gemstones;
    Coroutine showTooltipCoroutine;

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
      tooltipSystem.Show(header, content, gemstones);
    }
  }
}
