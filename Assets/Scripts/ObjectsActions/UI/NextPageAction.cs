using System;

namespace bts {
  public class NextPageAction : PageAction {
    public override string TootlipContent => "Next";
    public override Action Action => ShowNextPage;

    void ShowNextPage() {
      actionPanel.ShowNextPage();
      prevPageButton.EnableButton();
      if (actionPanel.CurrentPage == actionPanel.Pages - 1) {
        nextPageButton.DisableButton();
      }
    }
  }
}
