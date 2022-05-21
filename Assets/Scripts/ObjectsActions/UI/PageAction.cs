using UnityEngine;

namespace bts {
  public abstract class PageAction : UICommand {
    public SelectedObjectActionsPanel ActionPanel { get; private set; }
    public UICommandButton PrevPageButton { get; private set; }
    public UICommandButton NextPageButton { get; private set; }

    public PageAction(Sprite buttonIcon, TooltipData tooltipData, SelectedObjectActionsPanel actionPanel, UICommandButton prevPageButton, UICommandButton nextPageButton)
      : base(buttonIcon, tooltipData) {
      ActionPanel = actionPanel;
      PrevPageButton = prevPageButton;
      NextPageButton = nextPageButton;
    }
  }
}
