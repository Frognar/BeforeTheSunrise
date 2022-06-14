using System.Collections.Generic;
using bts.Gemstones;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace bts {
  public class Tooltip : MonoBehaviour {
    [SerializeField] InputReader inputReader;
    [SerializeField] TooltipSystem tooltipSystem;
    [SerializeField] TMP_Text headerField;
    [SerializeField] TMP_Text contentField;
    [SerializeField] RectTransform gemstonesRect;
    [SerializeField] Image sapphireImage;
    [SerializeField] TMP_Text sapphireField;
    [SerializeField] Image emeraldImage;
    [SerializeField] TMP_Text emeraldField;
    [SerializeField] Image rubbyImage;
    [SerializeField] TMP_Text rubbyField;
    [SerializeField] Image amethystImage;
    [SerializeField] TMP_Text amethystField;
    [SerializeField] Image topazImage;
    [SerializeField] TMP_Text topazField;
    [SerializeField] LayoutElement layoutElement;
    [SerializeField] int characterWrapLimit;
    RectTransform rectTransform;

    void Awake() {
      rectTransform = GetComponent<RectTransform>();
      tooltipSystem.Tooltip = this;
      gameObject.SetActive(false);
    }

    public void SetTooltip(TooltipData tooltipData) {
      SetText(tooltipData.Header, headerField);
      SetText(tooltipData.Content, contentField);
      SetGamestones(tooltipData.Gemstones);
      HandleLayoutElement();
      UpdatePosition(inputReader.ScreenPosition);
    }

    void SetText(string text, TMP_Text field) {
      if (string.IsNullOrEmpty(text)) {
        field.gameObject.SetActive(false);
      }
      else {
        field.gameObject.SetActive(true);
        field.text = text;
      }
    }

    void SetGamestones(IDictionary<GemstoneType, int> gemstones) {
      if (gemstones.Count == 0) {
        gemstonesRect.gameObject.SetActive(false);
      }
      else {
        gemstonesRect.gameObject.SetActive(true);
        SetGemstoneData(gemstones, GemstoneType.Sapphire, sapphireImage, sapphireField);
        SetGemstoneData(gemstones, GemstoneType.Emerald, emeraldImage, emeraldField);
        SetGemstoneData(gemstones, GemstoneType.Rubby, rubbyImage, rubbyField);
        SetGemstoneData(gemstones, GemstoneType.Amethyst, amethystImage, amethystField);
        SetGemstoneData(gemstones, GemstoneType.Topaz, topazImage, topazField);
      }
    }

    void SetGemstoneData(IDictionary<GemstoneType, int> gemstones, GemstoneType type, Image image, TMP_Text field) {
      if (gemstones.ContainsKey(type)) {
        image.gameObject.SetActive(true);
        field.gameObject.SetActive(true);
        field.text = gemstones[type].ToString();
      }
      else {
        image.gameObject.SetActive(false);
        field.gameObject.SetActive(false);
      }
    }

    void HandleLayoutElement() {
      int headerLength = headerField.text.Length;
      int contentLength = contentField.text.Length;
      layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit);
    }

    void OnEnable() {
      inputReader.ScreenPositionEvent += UpdatePosition;
    }
    
    void OnDisable() {
      inputReader.ScreenPositionEvent -= UpdatePosition;
    }

    void UpdatePosition(Vector2 position) {
      Vector2 pivot = new Vector2(position.x / Screen.width, position.y / Screen.height);
      transform.position = position;
      rectTransform.pivot = pivot;
    }
  }
}
