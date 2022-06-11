using UnityEngine;

namespace bts {
  public class HealerHealState : HealerBaseState {
    bool HasTarget => Context.Target != null && (Context.Target as Object) != null;
    bool IsTargetInRange => Vector3.Distance(Context.Target.Position, Context.Position) <= Context.Range;
    bool TargetIsIntact => Context.Target.IsIntact;

    readonly LaserParameters vfxParameters = new LaserParameters();

    public HealerHealState(StateMachine<Healer> stateMachine, StateFactory<Healer> factory)
      : base(stateMachine, factory) {
    }

    public override void UpdateState() {
      if (CheckSwitchState()) {
        return;
      }

      if (HasTarget && IsTargetInRange) {
        if (TargetIsIntact) {
          Context.Target = null;
          StateMachine.SwitchState(Factory.GetState(nameof(HealerIdleState)));
        }
        else if (Context.CanAfford(Context.EnergyPerHeal)) {
          SetSFX();
          SetVFX();
          Context.Target.Heal(Context.HealAmount);
          Context.UseEnergy(Context.EnergyPerHeal);
        }
        else {
          StopVFX();
          StopSFX();
        }
      }
      else {
        Context.Target = null;
        StateMachine.SwitchState(Factory.GetState(nameof(HealerIdleState)));
      }
    }

    public override void ExitState() {
      StopVFX();
      StopSFX();
    }

    void SetVFX() {
      if (Context.Laser == null) {
        vfxParameters.SourcePosition = Context.LaserBegining.position;
        vfxParameters.Target = Context.Target.Center;
        Context.Laser = Context.LaserEventChannel.RaiseGetEvent(Context.LaserConfig, vfxParameters);
      }
    }

    void StopVFX() {
      if (Context.Laser != null) {
        Context.Laser.Release();
        Context.Laser = null;
      }
    }

    void SetSFX() {
      if (Context.SoundEmitter == null) {
        Context.SoundEmitter = Context.SFXEventChannel.RaisePlayEventWithEmitter(
        Context.HealSFX,
        Context.AudioConfig,
        Context.Center.position
      );
      }
    }

    void StopSFX() {
      if (Context.SoundEmitter != null) {
        Context.SoundEmitter.Stop();
        Context.SoundEmitter = null;
      }
    }
  }
}
