using UnityEditor;
using UnityEngine;

namespace bts {
  public class PauseController : MonoBehaviour {
    [SerializeField] LoadSceneEventChannel loadSceneEventChannel;
    [SerializeField] InputReader inputReader;

    void OnEnable() {
      inputReader.OnPauseEvent += OnEnterPause;
    }

    void OnDisable() {
      inputReader.OnPauseEvent -= OnEnterPause;
    }

    void OnEnterPause() {
      loadSceneEventChannel.RaiseOnLoadScene(ScenesNames.InGameOptions, unloadCurrent: false);
    }
  }
}
