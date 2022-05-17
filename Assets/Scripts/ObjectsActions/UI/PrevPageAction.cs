using System;

namespace bts {
  public class PrevPageAction : PageAction {
    public override string TootlipContent => "Prev";
    public override Action Action => ShowPrevPage;

    void ShowPrevPage() {
      actionPanel.ShowPrevPage();
      nextPageButton.EnableButton();
      if (actionPanel.CurrentPage == 0) {
        prevPageButton.DisableButton();
      }
    }
  }
}
