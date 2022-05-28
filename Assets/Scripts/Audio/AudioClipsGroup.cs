using System;
using System.Collections.Generic;
using UnityEngine;

namespace bts {
  [Serializable]
  public class AudioClipsGroup {
    [field: SerializeField] public bool PickRandom { get; private set; }
    [field: SerializeField][Tooltip("Only if PickRandom is checked")] public bool AllowImmediateRepeat { get; private set; }
    [field: SerializeField] public List<AudioClip> Clips { get; private set; }

    int nextClipToPlay = -1;
    int lastClipPlayed = -1;

    public AudioClip GetClip() {
      if (Clips.Count == 1) {
        return Clips[0];
      }
      
      if (nextClipToPlay == -1) {
        nextClipToPlay = PickRandom ? UnityEngine.Random.Range(0, Clips.Count) : 0;
      }
      else {
        if (PickRandom) {
          if (AllowImmediateRepeat) {
            nextClipToPlay = UnityEngine.Random.Range(0, Clips.Count);
          }
          else {
            do {
              nextClipToPlay = UnityEngine.Random.Range(0, Clips.Count);
            } while (nextClipToPlay == lastClipPlayed);
          }
        }
        else {
          nextClipToPlay = ++nextClipToPlay % Clips.Count;
        }
      }

      lastClipPlayed = nextClipToPlay;
      return Clips[nextClipToPlay];
    }

    public static implicit operator AudioClip(AudioClipsGroup audioClips) => audioClips.GetClip();
  }
}