using bts.Gemstones;
using UnityEngine;
using UnityEngine.UI;

namespace bts {
  public class GemstoneGatherReward : MonoBehaviour {
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public int NightRequired { get; private set;  }
    [field: SerializeField] public GemstoneDictionary GemstoneReward { get; private set; }
    [SerializeField] TooltipTrigger trigger;
    [SerializeField] Image image;
    [SerializeField] Image buttonImage;
    [SerializeField] Button button;
    [SerializeField] Color disabledColor;
    [SerializeField] Color enabledColor;
    [SerializeField] Color selectedColor;

    void Start() {
      trigger.SetUp(new TooltipData(string.Empty, $"Requires survival of at least {NightRequired} nights", GemstoneReward));
      image.sprite = Icon;
    }
    
    public void Init(int survivedNights) {
      if (survivedNights >= NightRequired) {
        button.interactable = true;
        buttonImage.color = enabledColor;
      }
      else {
        button.interactable = false;
        buttonImage.color = disabledColor;
      }
    }

    public void Select() {
      buttonImage.color = selectedColor;
    }
    
    public void Deselect() {
      buttonImage.color = enabledColor;
    }
  }
}
