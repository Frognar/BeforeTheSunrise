using System;
using UnityEngine;

namespace bts {
  [CreateAssetMenu(fileName = "Scene Channel", menuName = "Channels/Scene Channel")]
  public class LoadSceneEventChannel : ScriptableObject {
    public Action<string, bool> OnLoadScene = delegate { };
    public Action<string> OnUnloadScene = delegate { };

    public void RaiseOnLoadScene(string sceneName, bool unloadCurrent = true) {
      OnLoadScene.Invoke(sceneName, unloadCurrent);
    }

    public void RaiseOnUnloadScene(string sceneName) {
      OnUnloadScene.Invoke(sceneName);
    }
  }
}
