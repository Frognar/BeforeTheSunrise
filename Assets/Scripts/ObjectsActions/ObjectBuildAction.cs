using System;
using UnityEngine;

namespace bts {
  [CreateAssetMenu(menuName = "ObjectActions/BuildAction", fileName = "BuildAction")]
  public class ObjectBuildAction : ObjectAction {
    public UnitCommander Commander { private get; set; }
    [SerializeField] PlacedObjectTypeSO buildingType;
    [SerializeField] string tooltipContent;
    [SerializeField] Sprite buttonIcon;
    public override string TootlipContent => tooltipContent;
    public override Sprite ButtonIcon => buttonIcon;
    public override string TootlipHeader => buildingType.objectName;
    public override GemstoneDictionary TootlipGemstones => (buildingType.customData as CustomBuildingData).buildingCosts;
    public override Action Action => () => Commander.SetBuildingToBuild(buildingType);
  }
}
