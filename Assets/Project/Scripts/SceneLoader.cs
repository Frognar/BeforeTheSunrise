using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace bts {
  public class SceneLoader : MonoBehaviour {
    [SerializeField] LoadSceneEventChannel loadSceneEventChannel;
    string activeScene;
    List<string> activeScenes;

    void Start() {
      activeScenes = new List<string>();
      LoadScene(ScenesNames.MainMenu);
    }

    void OnEnable() {
      loadSceneEventChannel.OnLoadScene += LoadScene;
      loadSceneEventChannel.OnUnloadScene += UnloadScene;
    }

    void OnDisable() {
      loadSceneEventChannel.OnLoadScene -= LoadScene;
      loadSceneEventChannel.OnUnloadScene -= UnloadScene;
    }

    void LoadScene(string sceneName, bool unloadCurrent = false) {
      if (activeScenes.Contains(sceneName)) {
        Debug.LogWarning("Scene " + sceneName + " is already loaded");
        return;
      }
      
      if (activeScene == null) {
        activeScene = sceneName;
      }
      else if (unloadCurrent) {
        UnloadScene(activeScene);
        activeScene = sceneName;
      }

      activeScenes.Add(sceneName);
      _ = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    void UnloadScene(string sceneName) {
      if (activeScenes.Contains(sceneName)) {
        _ = SceneManager.UnloadSceneAsync(sceneName);
        _ = activeScenes.Remove(sceneName);
      }
      else {
        Debug.LogWarning($"Scene {sceneName} is not loaded");
      }
    }
  }
}
