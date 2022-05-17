using System;
using UnityEngine;

namespace bts {
  public abstract class ObjectAction : ScriptableObject {
    public abstract string TootlipHeader { get; }
    public abstract string TootlipContent { get; }
    public abstract GemstoneDictionary TootlipGemstones { get; }
    public abstract Sprite ButtonIcon { get; }
    public abstract Action Action { get; }
  }
}
