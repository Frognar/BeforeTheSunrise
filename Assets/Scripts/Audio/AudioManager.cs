using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;

namespace bts {
  public class AudioManager : MonoBehaviour {
    [SerializeField] SFXEventChannel sfxEventChannel;
    [SerializeField] MusicEventChannel musicEventChannel;

    [SerializeField] AudioMixer audioMixer;
    [SerializeField] float masterVolume = 1f;
    [SerializeField] float musicVolume = 1f;
    [SerializeField] float sfxVolume = 1f;

    IObjectPool<SoundEmitter> emittersPool;
    SoundEmitter musicEmitter;

    void Awake() {
      emittersPool = new ObjectPool<SoundEmitter>(() => {
        SoundEmitter emitter = new GameObject("SoundEmitter").AddComponent<SoundEmitter>();
        emitter.transform.SetParent(transform);
        emitter.Pool = emittersPool;
        return emitter;
      });
    }

    void OnValidate() {
      if (Application.isPlaying) {
        SetGroupVolume("MasterVolume", masterVolume);
        SetGroupVolume("MusicVolume", musicVolume);
        SetGroupVolume("SFXVolume", sfxVolume);
      }
    }
    
    public void SetGroupVolume(string parameterName, float normalizedVolume) {
      _ = audioMixer.SetFloat(parameterName, NormalizedToMixerValue(normalizedVolume));
    }
    
    float NormalizedToMixerValue(float normalizedValue) {
      return (normalizedValue - 1f) * 80f;
    }

    void OnEnable() {
      sfxEventChannel.OnSFXPlayRequest += PlayAudioClip;
      musicEventChannel.OnMusicPlayRequest += PlayMusic;
      musicEventChannel.OnMusicStopRequest += StopMusic;
    }

    void OnDisable() {
      sfxEventChannel.OnSFXPlayRequest -= PlayAudioClip;
      musicEventChannel.OnMusicPlayRequest -= PlayMusic;
      musicEventChannel.OnMusicStopRequest -= StopMusic;
    }

    public void PlayAudioClip(AudioClip audioClip, AudioConfiguration config, Vector3 position) {
      SoundEmitter soundEmitter = emittersPool.Get();
      config.ApplyTo(soundEmitter.AudioSource);
      soundEmitter.PlayAudioClip(audioClip, loop: false, position);
    }

    public void PlayMusic(AudioClip audioClip, AudioConfiguration config) {
      const float fadeTime = 3f;
      if (musicEmitter != null) {
        if (musicEmitter.CurrentClip == audioClip && musicEmitter.IsPlaying) {
          return;
        }
        
        musicEmitter.FadeOut(fadeOutTime: fadeTime);
      }
      
      musicEmitter = emittersPool.Get();
      config.ApplyTo(musicEmitter.AudioSource);
      musicEmitter.FadeIn(audioClip, loop: true, fadeInTime: fadeTime);
    }

    public void StopMusic() {
      if (musicEmitter != null && musicEmitter.IsPlaying) {
        musicEmitter.Stop();
      }
    }
  }
}
