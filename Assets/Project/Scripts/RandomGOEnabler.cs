using System.Collections.Generic;
using UnityEngine;

namespace bts {
  public class RandomGOEnabler : MonoBehaviour {
    [SerializeField] List<GameObject> gameObjects;

    void Awake() {
      int enabledIndex = Random.Range(0, gameObjects.Count);
      for (int i = 0; i < gameObjects.Count; i++) {
        gameObjects[i].SetActive(i == enabledIndex);
      }
    }
  }
}
