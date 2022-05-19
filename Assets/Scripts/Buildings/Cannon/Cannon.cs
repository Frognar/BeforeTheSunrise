using System;
using UnityEngine;

namespace bts {
  public class Cannon : PlacedObject, ElectricDevice {
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float EnergyPerAttack { get; private set; }
    [field: SerializeField] public float Range { get; private set; }
    [field: SerializeField] public float MaxEnergy { get; private set; }
    [field: SerializeField] float ElectricDevice.CurrentEnergy { get; set; }
    public bool IsOrderedToStop { get; set; }
    public bool IsOrderedToAttack { get; set; }
    public Damageable Target { get; set; }
    public bool HasTarget => Target != null;
    public bool TargetInRange => Vector3.Distance(Target.Position, transform.position) <= Range;
    CannonStateFactory stateFactory;
    public CannonBaseState CurrentState { get; private set; }
    public bool IsSelected => Selected.activeSelf;
    public bool IsIdle { get; set; }

    protected override void Start() {
      base.Start();
      stateFactory = new CannonStateFactory(context: this);
      SwitchState(stateFactory.Idle);
    }

    public void SwitchState(CannonBaseState state) {
      CurrentState = state;
      CurrentState.EnterState();
    }

    void OnEnable() {
      TimeTicker.OnTick += OnTick;
    }

    void OnDisable() {
      TimeTicker.OnTick -= OnTick;
    }

    void OnTick(object sender, EventArgs e) {
      CurrentState?.UpdateState();
    }
  }
}
