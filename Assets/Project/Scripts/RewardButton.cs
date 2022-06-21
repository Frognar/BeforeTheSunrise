using fro.ValueAssets;
using UnityEngine;
using UnityEngine.UI;

namespace bts {

  public abstract class RewardButton : MonoBehaviour {
    [SerializeField] Sprite icon;
    [SerializeField] int nightRequired;
    [SerializeField] TooltipTrigger trigger;
    [SerializeField] Image image;
    [SerializeField] Image buttonImage;
    [SerializeField] Button button;
    [SerializeField] ColorAsset disabledColor;
    [SerializeField] ColorAsset normalColor;
    [SerializeField] ColorAsset selectedColor;
    protected string BaseTooltipContent => $"Requires survival of at least {nightRequired} nights";

    void Start() {
      image.sprite = icon;
      trigger.SetUp(CreateTooltip());
    }

    protected virtual TooltipData CreateTooltip() {
      return new TooltipData(string.Empty, BaseTooltipContent, null);
    }

    public void Init(int survivedNights) {
      if (survivedNights >= nightRequired) {
        button.interactable = true;
        buttonImage.color = normalColor;
      }
      else {
        button.interactable = false;
        buttonImage.color = disabledColor;
      }
    }

    public virtual void Select() {
      buttonImage.color = selectedColor;
    }

    public virtual void Deselect() {
      buttonImage.color = normalColor;
    }
  }
}
