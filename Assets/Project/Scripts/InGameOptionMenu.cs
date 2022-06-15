using UnityEngine;

namespace bts {
  public class InGameOptionMenu : OptionMenu {
    [SerializeField] Canvas inGameOptionCanves;
    [SerializeField] LoadSceneEventChannel loadSceneEventChannel;
    [SerializeField] InputReader inputReader;

    public void ShowMenu() {
      inputReader.EnableMenuInput();
      inputReader.DisableGameplayInput();
      inputReader.OnCloseEvent += CloseMenu;
      Time.timeScale = 0;
      inGameOptionCanves.enabled = true;
    }

    public void BackToMenu() {
      CloseMenu();
      loadSceneEventChannel.LoadMenu();
    }

    public void CloseMenu() {
      inGameOptionCanves.enabled = false;
      inputReader.EnableGameplayInput();
      inputReader.DisableMenuInput();
      inputReader.OnCloseEvent -= CloseMenu;
      Time.timeScale = 1f;
    }
  }
}
