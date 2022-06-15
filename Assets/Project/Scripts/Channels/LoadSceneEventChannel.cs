using System;
using UnityEngine;

namespace bts {
  [CreateAssetMenu(fileName = "Scene Channel", menuName = "Channels/Scene Channel")]
  public class LoadSceneEventChannel : ScriptableObject {
    public event EventHandler OnLoadGame;
    public event EventHandler OnLoadMenu;

    public void LoadGame(){
      OnLoadGame?.Invoke(this, EventArgs.Empty);
    }

    public void LoadMenu() {
      OnLoadMenu?.Invoke(this, EventArgs.Empty);
    }
  }
}
