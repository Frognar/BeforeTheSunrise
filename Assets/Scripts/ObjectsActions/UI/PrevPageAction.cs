using UnityEngine;

namespace bts {
  public class PrevPageAction : PageAction {
    public PrevPageAction(Sprite buttonIcon, TooltipData tooltipData, SelectedObjectActionsPanel actionPanel, UICommandButton prevPageButton, UICommandButton nextPageButton)
      : base(buttonIcon, tooltipData, actionPanel, prevPageButton, nextPageButton) {
    }

    public override void Execute() {
      ActionPanel.ShowPrevPage();
      NextPageButton.EnableButton();
      if (ActionPanel.CurrentPage == 0) {
        PrevPageButton.DisableButton();
      }
    }
  }
}
