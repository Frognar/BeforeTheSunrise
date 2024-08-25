using System;
using System.Collections;
using System.Collections.Generic;
using fro.bts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace bts {
  public class SceneLoader : MonoBehaviour {
    [SerializeField] LoadSceneEventChannel loadSceneEventChannel;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Image progressBarFill;

    void Start() {
      _ = SceneManager.LoadSceneAsync(ScenesNames.MainMenuScene, LoadSceneMode.Additive);
    }

    void OnEnable() {
      loadSceneEventChannel.OnLoadMenu += LoadMenu;
      loadSceneEventChannel.OnLoadGame += LoadGame;
    }

    void OnDisable() {
      loadSceneEventChannel.OnLoadMenu -= LoadMenu;
      loadSceneEventChannel.OnLoadGame -= LoadGame;
    }

    void LoadMenu(object sender, EventArgs e) {
      _ = SceneManager.UnloadSceneAsync(ScenesNames.GameScene);
      _ = SceneManager.LoadSceneAsync(ScenesNames.MainMenuScene, LoadSceneMode.Additive);
    }

    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    void LoadGame(object sender, EventArgs e) {
      loadingScreen.SetActive(true);
      scenesLoading.Add(SceneManager.UnloadSceneAsync(ScenesNames.MainMenuScene));
      scenesLoading.Add(SceneManager.LoadSceneAsync(ScenesNames.GameScene, LoadSceneMode.Additive));
      Time.timeScale = 1f;
      _ = StartCoroutine(GetSceneLoadProgress());
      _ = StartCoroutine(GetTotalProgress());
    }

    float totalSceneProgress;
    float totalMapGenerationProgress;
    IEnumerator GetSceneLoadProgress() {
      for (int i = 0; i < scenesLoading.Count; i++) {
        while (!scenesLoading[i].isDone) {
          totalSceneProgress = 0;

          foreach (AsyncOperation sceneLoading in scenesLoading) {
            totalSceneProgress += sceneLoading.progress;
          }

          totalSceneProgress /= scenesLoading.Count;
          yield return null;
        }
      }
      
      scenesLoading.Clear();
    }

    IEnumerator GetTotalProgress() {
      while (MapGenerator.instance == null || MapGenerator.instance.IsDone == false) {
        if (MapGenerator.instance == null) {
          totalMapGenerationProgress = 0f;
        }
        else {
          totalMapGenerationProgress = MapGenerator.instance.Progress;
        }
        
        progressBarFill.fillAmount = (totalSceneProgress + totalMapGenerationProgress) / 2;
        yield return null;
      }

      loadingScreen.SetActive(false);
    }
  }
}
