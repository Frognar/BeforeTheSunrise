using UnityEngine;

namespace bts {
  public class WinController : MonoBehaviour {
    [SerializeField] VoidEventChannel spawnerDeathEventChannel;
    [SerializeField] WinPanel winPanel;

    void OnEnable() {
      spawnerDeathEventChannel.OnEventInvoked += OnWin;
    }

    void OnDisable() {
      spawnerDeathEventChannel.OnEventInvoked -= OnWin;
    }

    void OnWin(object sender, System.EventArgs e) {
      winPanel.Show();
      Time.timeScale = 0;
    }
  }
}
