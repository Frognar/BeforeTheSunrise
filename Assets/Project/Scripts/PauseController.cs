using UnityEngine;

namespace bts {
  public class PauseController : MonoBehaviour {
    [SerializeField] InGameOptionMenu inGameOptionMenu;
    [SerializeField] InputReader inputReader;

    void OnEnable() {
      inputReader.OnPauseEvent += OnEnterPause;
    }

    void OnDisable() {
      inputReader.OnPauseEvent -= OnEnterPause;
    }

    void OnEnterPause() {
      inGameOptionMenu.ShowMenu();
    }
  }
}
