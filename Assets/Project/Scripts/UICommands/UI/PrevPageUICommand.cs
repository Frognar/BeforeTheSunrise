namespace bts {
  public class PrevPageUICommand : PageUICommand {
    public PrevPageUICommand(SimpleUICommandData data, SelectedObjectCommandsPanel actionPanel, UICommandButton prevPageButton, UICommandButton nextPageButton)
      : base(data, actionPanel, prevPageButton, nextPageButton) {
    }

    public override void Execute() {
      CommandsPanel.ShowPrevPage();
      NextPageButton.EnableButton();
      if (CommandsPanel.CurrentPage == 0) {
        PrevPageButton.DisableButton();
      }
    }
  }
}
