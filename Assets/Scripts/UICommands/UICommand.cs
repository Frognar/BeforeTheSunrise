using UnityEngine;

namespace bts {
  public abstract class UICommand : Command {
    public Sprite ButtonIcon { get; private set; }
    public TooltipData TooltipData { get; private set; }

    protected UICommand(Sprite buttonIcon, TooltipData tooltipData) {
      ButtonIcon = buttonIcon;
      TooltipData = tooltipData;
    }

    protected UICommand(UICommandData commandData) {
      ButtonIcon = commandData.ButtonIcon;
      TooltipData = commandData.TooltipData;
    }

    public abstract void Execute();
  }
}
