using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace bts {
  public class RewardPicker : MonoBehaviour {
    [SerializeField] VoidEventChannel onStartChannel;
    [SerializeField] LoadSceneEventChannel loadSceneEventChannel;
    [SerializeField] List<Button> buttons;
    [SerializeField] InputReader inputReader;
    [SerializeField] UnitStats unitStats;
    [SerializeField] Color disabledColor;
    [SerializeField] Color enabledColor;
    [SerializeField] Color selectedColor;
    int selectedIndex = -1;

    void Awake() {
      DisableAllButtons();
      const int rewardInterval = 5;
      int bestNight = PlayerPrefs.GetInt("Best", 0);
      for (int i = 1; i <= rewardInterval; i++) {
        if (bestNight >= i * rewardInterval) {
          buttons[i - 1].interactable = true;
          buttons[i - 1].GetComponent<Image>().color = enabledColor;
        }
        else {
          break;
        }
      }
    }

    void DisableAllButtons() {
      foreach (Button button in buttons) {
        button.interactable = false;
        button.GetComponent<Image>().color = disabledColor;
      }
    }

    public void StartGame() {
      inputReader.EnableGameplayInput();
      loadSceneEventChannel.RaiseOnUnloadScene(ScenesNames.RewardScene);
      onStartChannel.Invoke();
    }

    public void IncreaseGemstoneGathering(GemstoneGatherReward reward) {
      unitStats.ResetGatherBonus();
      unitStats.gatherBonuses.Add(reward.GemstoneReward);
    }

    public void SetSelectedButton(int index) {
      if (index < 0 || index >= buttons.Count) {
        return;
      }

      if (selectedIndex != -1) {
        buttons[selectedIndex].GetComponent<Image>().color = enabledColor;
      }

      selectedIndex = index;
      buttons[index].GetComponent<Image>().color = selectedColor;
    }
  }
}
