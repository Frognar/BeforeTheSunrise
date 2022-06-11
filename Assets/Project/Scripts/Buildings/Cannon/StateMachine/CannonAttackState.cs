using UnityEngine;

namespace bts {
  public class CannonAttackState : CannonBaseState {
    bool HasTarget => Context.Target != null && (Context.Target as Object) != null;
    bool IsTargetInRange => Vector3.Distance(Context.Target.Position, Context.Position) <= Context.Range;
    bool IsTimeToAttack => lastAttackTime + Context.TimeBetweenAttacks <= Time.time;
    float lastAttackTime;
    readonly ElectricArcParameters parameters = new ElectricArcParameters();

    public CannonAttackState(StateMachine<Cannon> stateMachine, StateFactory<Cannon> factory)
      : base(stateMachine, factory) {
    }

    public override void UpdateState() {
      if (CheckSwitchState()) {
        return;
      }

      if (HasTarget) {
        if (IsTargetInRange) {
          if (IsTimeToAttack) {
            if (Context.CanAfford(Context.EnergyPerAttack)) {
              lastAttackTime = Time.time;
              CreateVFX();
              Context.SFXEventChannel.RaisePlayEvent(Context.AttackSFX, Context.AudioConfig, Context.Position);
              Context.UseEnergy(Context.EnergyPerAttack);
              Context.Target.TakeDamage(Context.Damage);
              if (Context.Target.IsDead) {
                Context.Target = null;
                StateMachine.SwitchState(Factory.GetState(nameof(CannonIdleState)));
              }
            }
          }
        }
        else {
          Context.Target = null;
          StateMachine.SwitchState(Factory.GetState(nameof(CannonIdleState)));
        }
      }
    }
    
    void CreateVFX() {
      parameters.Source = Context.ArcBegin;
      parameters.TargetPosition = Context.Target.Center.position;
      Context.VFXEventChannel.RaiseSpawnEvent(Context.ElectricArcConfig, parameters);
    }
  }
}
