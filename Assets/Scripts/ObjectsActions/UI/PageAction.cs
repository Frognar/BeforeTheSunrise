using UnityEngine;

namespace bts {
  public abstract class PageAction : ObjectAction {
    public SelectedObjectActionsPanel actionPanel;
    public ObjectActionButton prevPageButton;
    public ObjectActionButton nextPageButton;
    public Sprite icon;
    readonly GemstoneDictionary emptyDict = new GemstoneDictionary();
    public override string TootlipHeader => string.Empty;
    public override GemstoneDictionary TootlipGemstones => emptyDict;
    public override Sprite ButtonIcon => icon;
  }
}
