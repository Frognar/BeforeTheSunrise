using UnityEngine;

namespace bts {
  public class HealerHealState : HealerBaseState {
    bool HasTarget => StateMachine.Context.Target != null && (StateMachine.Context.Target as Object) != null;
    bool TargetIsIntact => StateMachine.Context.Target.IsIntact;

    public HealerHealState(StateMachine<Healer> stateMachine, StateFactory<Healer> factory)
      : base(stateMachine, factory) {
    }

    public override void UpdateState() {
      if (CheckSwitchState()) {
        return;
      }

      if (HasTarget) {
        if (TargetIsIntact) {
          StateMachine.Context.Target = null;
          StateMachine.SwitchState(Factory.GetState(nameof(HealerIdleState)));
        }
        else if (StateMachine.Context.CanAfford(StateMachine.Context.EnergyPerHeal)) {
          SetSFX();
          SetVFX();
          StateMachine.Context.Target.Heal(StateMachine.Context.HealAmount);
          StateMachine.Context.UseEnergy(StateMachine.Context.EnergyPerHeal);
        }
        else {
          StopVFX();
          StopSFX();
        }
      }
      else {
        StateMachine.Context.Target = null;
        StateMachine.SwitchState(Factory.GetState(nameof(HealerIdleState)));
      }
    }

    public override void ExitState() {
      StopVFX();
      StopSFX();
    }

    void SetVFX() {
      if (StateMachine.Context.Laser == null) {
        StateMachine.Context.Laser = StateMachine.Context.LaserEventChannel.RaiseLaserEvent(
        StateMachine.Context.LaserBegining.position,
        StateMachine.Context.Target.Center,
        StateMachine.Context.LaserConfig
      );
      }
    }

    void StopVFX() {
      if (StateMachine.Context.Laser != null) {
        StateMachine.Context.Laser.Release();
        StateMachine.Context.Laser = null;
      }
    }

    void SetSFX() {
      if (StateMachine.Context.SoundEmitter == null) {
        StateMachine.Context.SoundEmitter = StateMachine.Context.SFXEventChannel.RaisePlayEventWithEmitter(
        StateMachine.Context.HealSFX,
        StateMachine.Context.AudioConfig,
        StateMachine.Context.Center.position
      );
      }
    }

    void StopSFX() {
      if (StateMachine.Context.SoundEmitter != null) {
        StateMachine.Context.SoundEmitter.Stop();
        StateMachine.Context.SoundEmitter = null;
      }
    }
  }
}