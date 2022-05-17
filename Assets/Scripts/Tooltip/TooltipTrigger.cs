using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace bts {
  public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    TooltipSystem tooltipSystem;
    [field: SerializeField] public string Header { get; private set; }
    [field: SerializeField] public string Content { get; private set; }
    [field: SerializeField] public GemstoneDictionary Gemstones { get; private set; }
    Coroutine showTooltipCoroutine;

    public void SetUp(string header, string content, GemstoneDictionary gemstones) {
      Header = header;
      Content = content;
      Gemstones = gemstones;
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
      tooltipSystem.Show(Header, Content, Gemstones);
    }
  }
}
