using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace bts {
  [RequireComponent(typeof(AudioSource))]
  public class SoundEmitter : MonoBehaviour {
    public IObjectPool<SoundEmitter> Pool { get; set; }
    public bool IsPlaying => AudioSource.isPlaying;
    public AudioClip CurrentClip => AudioSource.clip;
    public AudioSource AudioSource { get; private set; }

    void Awake() {
      AudioSource = GetComponent<AudioSource>();
    }

    public void PlayAudioClip(AudioClip clip, bool loop, Vector3 position = default) {
      AudioSource.clip = clip;
      AudioSource.transform.position = position;
      AudioSource.loop = loop;
      AudioSource.time = 0f;
      AudioSource.Play();
      if (!loop) {
        _ = StartCoroutine(FinishedPlaying(clip.length));
      }
    }

    public void FadeIn(AudioClip clip, bool loop, Vector3 position = default, float fadeInTime = 0.5f) {
      AudioSource.clip = clip;
      AudioSource.transform.position = position;
      AudioSource.loop = loop;
      AudioSource.time = 0f;
      AudioSource.Play();
      AudioSource.volume = 0f;
      _ = StartCoroutine(FadeInCoroutine(fadeInTime));
      
      if (!loop) {
        _ = StartCoroutine(FinishedPlaying(clip.length));
      }
    }

    public void FadeOut(float fadeOutTime = 0.5f) {
      _ = StartCoroutine(FadeOutCoroutine(fadeOutTime));
    }

    IEnumerator FinishedPlaying(float clipLength) {
      yield return new WaitForSeconds(clipLength);
      Pool.Release(this);
    }

    IEnumerator FadeInCoroutine(float fadeInTime) {
      float startTime = Time.time;
      float endTime = startTime + fadeInTime;
      while (Time.time < endTime) {
        AudioSource.volume = (Time.time - startTime) / fadeInTime;
        yield return null;
      }
      
      AudioSource.volume = 1f;
    }

    IEnumerator FadeOutCoroutine(float fadeOutTime) {
      float startTime = Time.time;
      float endTime = startTime + fadeOutTime;
      while (Time.time < endTime) {
        AudioSource.volume = 1f - (Time.time - startTime) / fadeOutTime;
        yield return null;
      }

      AudioSource.volume = 0f;
      AudioSource.Stop();
      Pool.Release(this);
    }

    public void Stop() {
      AudioSource.Stop();
      Pool.Release(this);
    }
  }
}