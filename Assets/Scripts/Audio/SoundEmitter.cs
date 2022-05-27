using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;

namespace bts {
  [RequireComponent(typeof(AudioSource))]
  public class SoundEmitter : MonoBehaviour {
    public IObjectPool<SoundEmitter> Pool { get; set; }
    public bool IsPlaying => audioSource.isPlaying;
    AudioSource audioSource;

    void Awake() {
      audioSource = GetComponent<AudioSource>();
    }

    public void SetAudioMixer(AudioMixerGroup audioMixerGroup) {
      audioSource.outputAudioMixerGroup = audioMixerGroup;
    }

    public void PlayAudioClip(AudioClip clip, bool hasToLoop, Vector3 position = default) {
      audioSource.clip = clip;
      audioSource.transform.position = position;
      audioSource.loop = hasToLoop;
      audioSource.time = 0f;
      audioSource.Play();
      if (!hasToLoop) {
        _ = StartCoroutine(FinishedPlaying(clip.length));
      }
    }

    IEnumerator FinishedPlaying(float clipLength) {
      yield return new WaitForSeconds(clipLength);
      Pool.Release(this);
    }

    public void Stop() {
      audioSource.Stop();
      Pool.Release(this);
    }
  }
}