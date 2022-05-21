using System.Collections.Generic;
using UnityEngine;

namespace bts {
  public class Wall : PlacedObject {
    [SerializeField] GemstoneStorage storage;
    [SerializeField] DemolishUICommandData demolishUICommandData;

    protected override void Start() {
      base.Start();
      UICommands = new List<UICommand>() { new DemolishUICommand(demolishUICommandData, this, storage) };
    }
  }
}
