using UnityEngine;

namespace bts {
  public class RewardPanel : MonoBehaviour {
    [SerializeField] VoidEventChannel onStartChannel;
    [SerializeField] Canvas rewardCanvas;
    [SerializeField] InputReader inputReader;
    [SerializeField] UnitStats unitStats;
    GemstoneGatherReward[] gemstoneGatherRewardButtons;
    int gemstoneGatherRewardButtonSelectedIndex = -1;
    SpecialRewardButton[] specialRewardButtons;
    int specialRewardButtonSelectedIndex = -1;

    void Awake() {
      inputReader.DisableGameplayInput();
      gemstoneGatherRewardButtons = GetComponentsInChildren<GemstoneGatherReward>(includeInactive: true);
      specialRewardButtons = GetComponentsInChildren<SpecialRewardButton>(includeInactive: true);
      int bestNight = PlayerPrefs.GetInt("Best", 0);
      foreach (GemstoneGatherReward button in gemstoneGatherRewardButtons) {
        button.Init(bestNight);
      }
      
      foreach (SpecialRewardButton button in specialRewardButtons) {
        button.Init(bestNight);
      }

      rewardCanvas.enabled = true;
    }

    public void StartGame() {
      inputReader.EnableGameplayInput();
      rewardCanvas.enabled = false;
      onStartChannel.Invoke();
    }

    public void IncreaseGemstoneGathering(GemstoneGatherReward reward) {
      unitStats.ResetGatherBonus();
      unitStats.gatherBonuses.Add(reward.GemstoneReward);
    }

    public void SetSelectedGemstoneGatherRewardButton(int index) {
      if (IsValid(index, gemstoneGatherRewardButtons.Length)) {
        if (IsSomethingSelected(gemstoneGatherRewardButtonSelectedIndex)) {
          gemstoneGatherRewardButtons[gemstoneGatherRewardButtonSelectedIndex].Deselect();
        }

        gemstoneGatherRewardButtonSelectedIndex = index;
        gemstoneGatherRewardButtons[gemstoneGatherRewardButtonSelectedIndex].Select();
      }
    }

    public void SetSelectedSpecialRewardButton(int index) {
      if (IsValid(index, specialRewardButtons.Length)) {
        if (IsSomethingSelected(specialRewardButtonSelectedIndex)) {
          specialRewardButtons[specialRewardButtonSelectedIndex].Deselect();
        }

        specialRewardButtonSelectedIndex = index;
        specialRewardButtons[specialRewardButtonSelectedIndex].Select();
      }
    }
    
    bool IsValid(int index, int maxIndes) {
      return index >= 0 && index < maxIndes;
    }

    bool IsSomethingSelected(int selectedIndex) {
      return selectedIndex != -1;
    }
  }
}
