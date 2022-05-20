using System;
using UnityEngine;

namespace bts {
  [CreateAssetMenu(menuName = "ObjectActions/CancelBuildAction", fileName = "CancelBuildAction")]
  public class ObjectCancelBuildAction : ObjectAction {
    public UnitCommander Commander { private get; set; }
    public override string TootlipHeader => string.Empty;
    public override string TootlipContent => "Cancel build action";
    readonly GemstoneDictionary emptyDict = new GemstoneDictionary();
    public override GemstoneDictionary TootlipGemstones => emptyDict;
    [SerializeField] Sprite buttonIcon;
    public override Sprite ButtonIcon => buttonIcon;
    public override Action Action => () => Commander.ClearBuildingToBuild();
  }
}
