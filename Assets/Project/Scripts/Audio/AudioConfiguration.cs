using UnityEngine;
using UnityEngine.Audio;

namespace bts {
  [CreateAssetMenu(fileName = "Audio Configuration", menuName = "Audio/Audio Configuration")]
  public class AudioConfiguration : ScriptableObject {
    [field: SerializeField] public AudioMixerGroup OutputAudioMixerGroup { get; private set; } = null;
    [field: SerializeField] public PriorityLevel PriorityLvl { get; private set; } = PriorityLevel.Standard;
    public int Priority => (int)PriorityLvl;

    [field: Header("Sound properties")]
    [field: SerializeField] public bool Mute { get; private set; } = false;
    [field: SerializeField][field: Range(0f, 1f)] public float Volume { get; private set; } = 1f;
    [field: SerializeField][field: Range(0f, 1f)] public float Pitch { get; private set; } = 1f;
    [field: SerializeField][field: Range(0f, 1f)] public float PanStereo { get; private set; } = 0f;
    [field: SerializeField][field: Range(0f, 1f)] public float ReverbZoneMix { get; private set; } = 1f;

    [field: Header("3D")]
    [field: SerializeField] public float SpatialBlend { get; private set; } = 1f;
    [field: SerializeField] public AudioRolloffMode RolloffMode { get; private set; } = AudioRolloffMode.Logarithmic;
    [field: SerializeField] public float MinDistance { get; private set; } = 1f;
    [field: SerializeField] public float MaxDistance { get; private set; } = 500f;
    [field: SerializeField] public int Spread { get; private set; } = 0;
    [field: SerializeField] public float DopplerLevel { get; private set; } = 1f;

    [field: Header("Ignores")]
    [field: SerializeField] public bool BypassEffects { get; private set; } = false;
    [field: SerializeField] public bool BypassListenerEffects { get; private set; } = false;
    [field: SerializeField] public bool BypassReverbZones { get; private set; } = false;
    [field: SerializeField] public bool IgnoreListenerVolume { get; private set; } = false;
    [field: SerializeField] public bool IgnoreListenerPause { get; private set; } = false;

    public enum PriorityLevel {
      Highest = 0,
      High = 64,
      Standard = 128,
      Low = 194,
      VeryLow = 256,
    }
    
    public void ApplyTo(AudioSource audioSource) {
      audioSource.outputAudioMixerGroup = OutputAudioMixerGroup;
      audioSource.mute = Mute;
      audioSource.bypassEffects = BypassEffects;
      audioSource.bypassListenerEffects = BypassListenerEffects;
      audioSource.bypassReverbZones = BypassReverbZones;
      audioSource.priority = Priority;
      audioSource.volume = Volume;
      audioSource.pitch = Pitch;
      audioSource.panStereo = PanStereo;
      audioSource.spatialBlend = SpatialBlend;
      audioSource.reverbZoneMix = ReverbZoneMix;
      audioSource.dopplerLevel = DopplerLevel;
      audioSource.spread = Spread;
      audioSource.rolloffMode = RolloffMode;
      audioSource.minDistance = MinDistance;
      audioSource.maxDistance = MaxDistance;
      audioSource.ignoreListenerVolume = IgnoreListenerVolume;
      audioSource.ignoreListenerPause = IgnoreListenerPause;
    }
  }
}
