using fro.ValueAssets;
using UnityEngine;

namespace bts {
  public class DeathScene : MonoBehaviour {
    [SerializeField] LoadSceneEventChannel loadSceneEventChannel;
    [SerializeField] InputReader inputReader;
    [SerializeField] IntAsset dayCounter;
    [SerializeField] TMPro.TMP_Text survived;
    [SerializeField] TMPro.TMP_Text best;

    void OnEnable() {
      inputReader.DisableGameplayInput();
      int bestNight = PlayerPrefs.GetInt("Best", 0);
      if (bestNight < dayCounter - 1) {
        bestNight = dayCounter - 1;
        PlayerPrefs.SetInt("Best", bestNight);
      }

      survived.text = $"Survived {dayCounter.value - 1} nights";
      best.text = $"Best: {bestNight}";
    }

    public void BackToMenu() {
      loadSceneEventChannel.RaiseOnLoadScene(ScenesNames.MainMenu);
      loadSceneEventChannel.RaiseOnUnloadScene(ScenesNames.DeathScene);
    }
  }
}
