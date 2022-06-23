using System.Collections.Generic;
using UnityEngine;

namespace bts {
  public class Drill : Building, ElectricDevice {
    Damageable spawner;
    bool shouldAttack;

    [Header("VFX")]
    [SerializeField] LaserEventChannel laserEventChannel;
    [SerializeField] Transform laserBegining;
    [SerializeField] LaserConfiguration laserConfig;
    readonly LaserParameters vfxParameters = new LaserParameters();
    LaserVFX laser;

    [Header("SFX")]
    [SerializeField] SFXEventChannel sfxEventChannel;
    [SerializeField] AudioConfiguration audioConfig;
    [SerializeField] AudioClipsGroup laserSFX;
    SoundEmitter soundEmitter;

    [field: Header("Energy")]
    [field: SerializeField] public float MaxEnergy { get; private set; }
    public float CurrentEnergy { get; private set; }
    [SerializeField] float energyPerAttack;
    [SerializeField] float damage;

    protected override void Awake() {
      base.Awake();
      spawner = FindObjectOfType<EnemySpawner>();
    }

    void Start() {
      WinGame();
    }

    public void WinGame() {
      shouldAttack = true;
    }

    void Update() {
      if (shouldAttack) {
        if (CanAfford(energyPerAttack)) {
          StartVFX();
          StartSFX();
          UseEnergy(energyPerAttack);
          spawner.TakeDamage(damage * Time.deltaTime);
        }
        else {
          StopVFX();
          StopSFX();
        }
      }
    }

    void StartVFX() {
      if (laser == null) {
        vfxParameters.SourcePosition = laserBegining.position;
        vfxParameters.Target = spawner.Center;
        laser = laserEventChannel.RaiseGetEvent(laserConfig, vfxParameters);
      }
    }

    void StopVFX() {
      if (laser != null) {
        laser.Release();
        laser = null;
      }
    }

    void StartSFX() {
      if (soundEmitter == null) {
        soundEmitter = sfxEventChannel.RaisePlayEventWithEmitter(laserSFX, audioConfig, Center.position);
      }
    }

    void StopSFX() {
      if (soundEmitter != null) {
        soundEmitter.Stop();
        soundEmitter = null;
      }
    }

    public bool CanAfford(float energy) {
      return CurrentEnergy >= energy;
    }

    public void UseEnergy(float energy) {
      if (CurrentEnergy <= 0) {
        return;
      }

      CurrentEnergy -= energy;
      if (CurrentEnergy < 0) {
        CurrentEnergy = 0;
      }

      InformAboutEnergyChange();
    }

    public void StoreEnergy(float energy) {
      if (CurrentEnergy >= MaxEnergy) {
        return;
      }

      CurrentEnergy += energy;
      if (CurrentEnergy > MaxEnergy) {
        CurrentEnergy = MaxEnergy;
      }

      InformAboutEnergyChange();
    }

    void InformAboutEnergyChange() {
      InvokeDataChange(new Dictionary<DataType, object>() {
        { DataType.CurrentEnergy, CurrentEnergy },
        { DataType.MaxEnergy, MaxEnergy }
      });
    }

    public override bool IsSameAs(Selectable other) {
      return other is Drill;
    }

    protected override void OnDestroy() {
      base.OnDestroy();
      StopVFX();
      StopSFX();
    }

    public override Dictionary<DataType, object> GetData() {
      Dictionary<DataType, object> data = base.GetData();
      data.Add(DataType.MaxEnergy, MaxEnergy);
      data.Add(DataType.CurrentEnergy, CurrentEnergy);
      data.Add(DataType.DamagePerSecond, damage);
      data.Add(DataType.EnergyUsagePerSecond, energyPerAttack);
      return data;
    }
  }
}
