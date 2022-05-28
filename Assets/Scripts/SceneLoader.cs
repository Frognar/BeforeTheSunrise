using UnityEngine;
using UnityEngine.SceneManagement;

namespace bts {
  public class SceneLoader : MonoBehaviour {
    void Awake() {
      SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }
  }
}
