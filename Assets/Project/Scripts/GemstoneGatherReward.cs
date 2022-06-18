using bts.Gemstones;
using fro.ValueAssets;
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
    [SerializeField] ColorAsset disabledColor;
    [SerializeField] ColorAsset normalColor;
    [SerializeField] ColorAsset selectedColor;

    void Start() {
      trigger.SetUp(new TooltipData(string.Empty, $"Requires survival of at least {NightRequired} nights", GemstoneReward));
      image.sprite = Icon;
    }
    
    public void Init(int survivedNights) {
      if (survivedNights >= NightRequired) {
        button.interactable = true;
        buttonImage.color = normalColor;
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
      buttonImage.color = normalColor;
    }
  }
}
