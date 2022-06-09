﻿using UnityEditor;
using UnityEngine;

namespace bts {
  public class InGameOptionMenu : OptionMenu {
    [SerializeField] LoadSceneEventChannel loadSceneEventChannel;
    [SerializeField] InputReader inputReader;

    void OnEnable() {
      inputReader.EnableMenuInput();
      inputReader.OnCloseEvent += CloseMenu;
      Time.timeScale = 0;
    }

    public void BackToMenu() {
      loadSceneEventChannel.RaiseOnLoadScene(ScenesNames.MainMenu);
      loadSceneEventChannel.RaiseOnUnloadScene(ScenesNames.InGameOptions);
    }

    public void CloseMenu() {
      inputReader.EnableGameplayInput();
      loadSceneEventChannel.RaiseOnUnloadScene(ScenesNames.InGameOptions);
    }

    void OnDisable() {
      inputReader.OnCloseEvent -= CloseMenu;
      Time.timeScale = 1f;
    }
  }
}