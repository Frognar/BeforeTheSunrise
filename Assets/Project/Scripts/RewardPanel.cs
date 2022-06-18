using UnityEngine;

namespace bts {
  public class RewardPanel : MonoBehaviour {
    [SerializeField] VoidEventChannel onStartChannel;
    [SerializeField] Canvas rewardCanvas;
    [SerializeField] InputReader inputReader;
    [SerializeField] UnitStats unitStats;
    GemstoneGatherReward[] rewardButtons;
    int selectedIndex = -1;

    void Awake() {
      inputReader.DisableGameplayInput();
      rewardButtons = GetComponentsInChildren<GemstoneGatherReward>(includeInactive: true);
      int bestNight = PlayerPrefs.GetInt("Best", 0);
      foreach (GemstoneGatherReward button in rewardButtons) {
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

    public void SetSelectedButton(int index) {
      if (IsValid(index)) {
        DeselectCurrent();
        Select(index);
      }
    }
    
    bool IsValid(int index) {
      return index >= 0 && index < rewardButtons.Length;
    }
    
    void DeselectCurrent() {
      if (IsSomeButtonSelected()) {
        rewardButtons[selectedIndex].Deselect();
      }
    }
    
    bool IsSomeButtonSelected() {
      return selectedIndex != -1;
    }

    void Select(int index) {
      selectedIndex = index;
      rewardButtons[selectedIndex].Select();
    }
  }
}
