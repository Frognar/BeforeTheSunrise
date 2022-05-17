using System;
using UnityEngine;

namespace bts {
  [CreateAssetMenu()]
  public class ObjectBuildAction : ObjectAction {
    public UnitCommander Commander { private get; set; }
    [SerializeField] PlacedObjectTypeSO buildingType;
    [SerializeField] string tooltipContent;
    [SerializeField] Sprite buttonIcon;
    public override string TootlipContent => tooltipContent;
    public override Sprite ButtonIcon => buttonIcon;
    public override string TootlipHeader => buildingType.objectName;
    public override GemstoneDictionary TootlipGemstones => buildingType.gemstoneCosts;
    public override Action Action => () => Commander.SetBuildingToBuild(buildingType);
  }
}
