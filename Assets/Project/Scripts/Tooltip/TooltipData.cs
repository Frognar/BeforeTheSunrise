using System;
using bts.Gemstones;
using UnityEngine;

namespace bts {
  [Serializable]
  public struct TooltipData {
    [field: SerializeField] public string Header { get; private set; }
    [field: SerializeField] public string Content { get; private set; }
    [field: SerializeField] public GemstoneDictionary Gemstones { get; private set; }

    public TooltipData(string header, string content, GemstoneDictionary gemstones) {
      Header = header;
      Content = content;
      Gemstones = gemstones;
    }

    public void UpdateGemstones(GemstoneDictionary gemstones) { 
      Gemstones = gemstones;
    }
  }
}
