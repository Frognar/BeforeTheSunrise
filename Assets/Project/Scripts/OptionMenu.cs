using UnityEngine;
using UnityEngine.UI;

namespace bts {
  public class OptionMenu : MonoBehaviour {
    [SerializeField] FloatEventChannel masterVolumeChannel;
    [SerializeField] FloatEventChannel musicVolumeChannel;
    [SerializeField] FloatEventChannel sfxVolumeChannel;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    
    void Start() {
      LoadMasterVolume();
      LoadMusicVolume();
      LoadSFXVolume();
    }

    void OnEnable() {
      masterVolumeChannel.OnEventRaised += SaveMasterVolume;
      musicVolumeChannel.OnEventRaised += SaveMusicVolume;
      sfxVolumeChannel.OnEventRaised += SaveSFXVolume;
    }

    void OnDisable() {
      masterVolumeChannel.OnEventRaised -= SaveMasterVolume;
      musicVolumeChannel.OnEventRaised -= SaveMusicVolume;
      sfxVolumeChannel.OnEventRaised -= SaveSFXVolume;
    }

    void LoadMasterVolume() {
      masterSlider.value = LoadVolume("Master");
    }

    void LoadMusicVolume() {
      musicSlider.value = LoadVolume("Music");
    }

    void LoadSFXVolume() {
      sfxSlider.value = LoadVolume("SFX");
    }

    float LoadVolume(string name) {
      return PlayerPrefs.GetFloat(name, .75f);
    }

    void SaveMasterVolume(float value) {
      SaveVolume("Master", value);
    }

    void SaveMusicVolume(float value) {
      SaveVolume("Music", value);
    }

    void SaveSFXVolume(float value) {
      SaveVolume("SFX", value);
    }

    void SaveVolume(string name, float value) {
      PlayerPrefs.SetFloat(name, value);
    }
  }
}
