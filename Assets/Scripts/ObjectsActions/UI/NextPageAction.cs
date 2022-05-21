using UnityEngine;

namespace bts {
  public class NextPageAction : PageAction {
    public NextPageAction(Sprite buttonIcon, TooltipData tooltipData, SelectedObjectActionsPanel actionPanel, UICommandButton prevPageButton, UICommandButton nextPageButton)
      : base(buttonIcon, tooltipData, actionPanel, prevPageButton, nextPageButton) {
    }

    public override void Execute() {
      ActionPanel.ShowNextPage();
      PrevPageButton.EnableButton();
      if (ActionPanel.CurrentPage == ActionPanel.Pages - 1) {
        NextPageButton.DisableButton();
      }
    }
  }
}
