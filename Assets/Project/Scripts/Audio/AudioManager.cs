using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;

namespace bts {
  public class AudioManager : MonoBehaviour {
    [SerializeField] FloatEventChannel masterVolumeChannel;
    [SerializeField] FloatEventChannel sfxVolumeChannel;
    [SerializeField] SFXEventChannel sfxEventChannel;
    [SerializeField] FloatEventChannel musicVolumeChannel;
    [SerializeField] MusicEventChannel musicEventChannel;

    [SerializeField] AudioMixer audioMixer;

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

    public void SetGroupVolume(string parameterName, float normalizedVolume) {
      _ = audioMixer.SetFloat(parameterName, NormalizedToMixerValue(normalizedVolume));
    }

    float NormalizedToMixerValue(float normalizedValue) {
      return normalizedValue > 0 ? Mathf.Log10(normalizedValue) * 20 : -80f;
    }

    void OnEnable() {
      masterVolumeChannel.OnEventRaised += SetMasterVolume;
      sfxEventChannel.OnSFXPlayRequest += PlayAudioClip;
      sfxEventChannel.OnSFXPlayRequestWithEmitter += PlayAudioClipAndReturnEmitter;
      sfxVolumeChannel.OnEventRaised += SetSFXVolume;
      musicEventChannel.OnMusicPlayRequest += PlayMusic;
      musicEventChannel.OnMusicStopRequest += StopMusic;
      musicVolumeChannel.OnEventRaised += SetMusicVolume;
    }

    void OnDisable() {
      masterVolumeChannel.OnEventRaised -= SetMasterVolume;
      sfxEventChannel.OnSFXPlayRequest -= PlayAudioClip;
      sfxEventChannel.OnSFXPlayRequestWithEmitter -= PlayAudioClipAndReturnEmitter;
      sfxVolumeChannel.OnEventRaised -= SetSFXVolume;
      musicEventChannel.OnMusicPlayRequest -= PlayMusic;
      musicEventChannel.OnMusicStopRequest -= StopMusic;
      musicVolumeChannel.OnEventRaised -= SetMusicVolume;
    }

    void SetMasterVolume(float volume) {
      SetGroupVolume("MasterVolume", volume);
    }

    public void PlayAudioClip(AudioClip audioClip, AudioConfiguration config, Vector3 position) {
      if (audioClip == null) {
        return;
      }

      SoundEmitter soundEmitter = emittersPool.Get();
      config.ApplyTo(soundEmitter.AudioSource);
      soundEmitter.PlayAudioClip(audioClip, loop: false, position);
    }

    public SoundEmitter PlayAudioClipAndReturnEmitter(AudioClip audioClip, AudioConfiguration config, Vector3 position) {
      if (audioClip == null) {
        return null;
      }

      SoundEmitter soundEmitter = emittersPool.Get();
      config.ApplyTo(soundEmitter.AudioSource);
      soundEmitter.PlayAudioClip(audioClip, loop: true, position);
      return soundEmitter;
    }

    void SetSFXVolume(float volume) {
      SetGroupVolume("SFXVolume", volume);
    }

    public void PlayMusic(AudioClip audioClip, AudioConfiguration config) {
      if (audioClip == null) {
        return;
      }

      const float fadeTime = 3f;
      if (musicEmitter != null) {
        if (musicEmitter.CurrentClip == audioClip && musicEmitter.IsPlaying) {
          return;
        }

        if (musicEmitter.IsPlaying) {
          musicEmitter.FadeOut(fadeOutTime: fadeTime);
        }
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

    void SetMusicVolume(float volume) {
      SetGroupVolume("MusicVolume", volume);
    }
  }
}
