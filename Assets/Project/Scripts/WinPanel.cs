using UnityEngine;

namespace bts {
  public class WinPanel : MonoBehaviour {
    [SerializeField] Canvas winCanvas;
    [SerializeField] LoadSceneEventChannel loadSceneEventChannel;
    [SerializeField] InputReader inputReader;

    public void Show() {
      winCanvas.enabled = true;
      inputReader.DisableGameplayInput();
    }

    public void BackToMenu() {
      loadSceneEventChannel.LoadMenu();
    }
  }
}
