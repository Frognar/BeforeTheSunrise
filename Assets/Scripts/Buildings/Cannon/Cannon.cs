using System;
using UnityEngine;

namespace bts {
  public class Cannon : PlacedObject, ElectricDevice {
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float EnergyPerAttack { get; private set; }
    public float Range => (placedObjectType.customData as CannonData).range;
    [field: SerializeField] public float MaxEnergy { get; private set; }
    [field: SerializeField] float ElectricDevice.CurrentEnergy { get; set; }
    public bool IsOrderedToStop { get; set; }
    public bool IsOrderedToAttack { get; set; }
    public Damageable Target { get; set; }
    public bool HasTarget => Target != null;
    public bool TargetInRange => Vector3.Distance(Target.Position, Position) <= Range;
    CannonStateFactory stateFactory;
    public CannonBaseState CurrentState { get; private set; }
    public bool IsSelected { get; private set; }
    public bool IsIdle { get; set; }
    GameObject rangeVisuals;

    protected override void Start() {
      base.Start();
      stateFactory = new CannonStateFactory(context: this);
      SwitchState(stateFactory.Idle);
      rangeVisuals = transform.Find("Range").gameObject;
      rangeVisuals.transform.localScale = new Vector3(Range * 2, Range * 2, 1f);
    }

    public void SwitchState(CannonBaseState state) {
      CurrentState = state;
      CurrentState.EnterState();
    }

    void OnEnable() {
      TimeTicker.OnSecond += OnSecond;
    }

    void OnDisable() {
      TimeTicker.OnSecond -= OnSecond;
    }

    void OnSecond(object sender, EventArgs e) {
      CurrentState?.UpdateState();
    }

    public override void Select() {
      Selected.SetActive(true);
      rangeVisuals.SetActive(true);
      IsSelected = true;
    }

    public override void Deselect() {
      Selected.SetActive(false);
      rangeVisuals.SetActive(false);
      IsSelected = false;
    }
  }
}
