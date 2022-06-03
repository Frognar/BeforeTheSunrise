namespace bts {
  public class NextPageUICommand : PageUICommand {
    public NextPageUICommand(SimpleUICommandData data, SelectedObjectCommandsPanel actionPanel, UICommandButton prevPageButton, UICommandButton nextPageButton)
      : base(data, actionPanel, prevPageButton, nextPageButton) {
    }

    public override void Execute() {
      CommandsPanel.ShowNextPage();
      PrevPageButton.EnableButton();
      if (CommandsPanel.CurrentPage == CommandsPanel.Pages - 1) {
        NextPageButton.DisableButton();
      }
    }
  }
}
