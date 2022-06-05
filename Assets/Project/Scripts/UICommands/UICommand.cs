using UnityEngine;

namespace bts {
  public abstract class UICommand : Command {
    public Sprite ButtonIcon => CommandData.ButtonIcon;
    public virtual TooltipData TooltipData => CommandData.TooltipData;
    public UICommandData CommandData { get; }

    protected UICommand(UICommandData commandData) {
      CommandData = commandData;
    }

    public abstract void Execute();
  }
}
