using System.Collections;
using UnityEditor;
using UnityEngine;

namespace bts {
  public class MenuScene : MonoBehaviour {
    [SerializeField] MusicEventChannel musicEventChannel;
    [SerializeField] AudioClipsGroup musics;
    [SerializeField] AudioConfiguration audioConfiguration;
    [SerializeField] LoadSceneEventChannel loadSceneEventChannel;
    [SerializeField] InputReader inputReader;
    Coroutine musicCoroutine;

    void Start() {
      inputReader.EnableMenuInput();
      RequestPlayMusic();
    }
    
    void RequestPlayMusic() {
      AudioClip clip = musics.GetClip();
      musicEventChannel.RaisePlayEvent(clip, audioConfiguration);
      musicCoroutine = StartCoroutine(NextRequest(clip.length));
    }
    
    IEnumerator NextRequest(float delay) {
      yield return new WaitForSeconds(delay);
      musicCoroutine = null;
      RequestPlayMusic();
    }
    
    public void RunGame() {
      inputReader.EnableGameplayInput();
      if (musicCoroutine != null) {
        StopCoroutine(musicCoroutine);
      }
      
      loadSceneEventChannel.RaiseOnLoadScene(ScenesNames.GameScene);
    }

    public void Exit() {
      Application.Quit();
    }
  }
}
