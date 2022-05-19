namespace bts {
  public class CannonAttackState : CannonBaseState {
    public CannonAttackState(Cannon context, CannonStateFactory factory)
      : base(context, factory) {
    }

    public override void UpdateState() {
      if (CheckSwitchState()) {
        return;
      }

      if (Context.HasTarget) {
        if (Context.TargetInRange) {
          if (ElectricContext.CanAfford(Context.EnergyPerAttack)) {
            Context.Target.TakeDamage(Context.Damage);
            if (Context.Target.IsDead) {
              Context.Target = null;
              SwitchState(Factory.Idle);
            }
          }
        }
        else {
          Context.Target = null;
          SwitchState(Factory.Idle);
        }
      }
    }
  }
}
