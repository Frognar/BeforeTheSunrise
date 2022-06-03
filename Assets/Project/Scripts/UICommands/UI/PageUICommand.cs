namespace bts {
  public abstract class PageUICommand : UICommand {
    public SelectedObjectCommandsPanel CommandsPanel { get; private set; }
    public UICommandButton PrevPageButton { get; private set; }
    public UICommandButton NextPageButton { get; private set; }

    public PageUICommand(SimpleUICommandData data, SelectedObjectCommandsPanel commandsPanel, UICommandButton prevPageButton, UICommandButton nextPageButton)
      : base(data.ButtonIcon, data.TooltipData) {
      CommandsPanel = commandsPanel;
      PrevPageButton = prevPageButton;
      NextPageButton = nextPageButton;
    }
  }
}
