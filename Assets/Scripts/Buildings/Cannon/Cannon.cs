using System.Collections.Generic;
using UnityEngine;

namespace bts {
  public class Cannon : PlacedObject, ElectricDevice {
    [SerializeField] GemstoneStorage storage;
    [SerializeField] DemolishUICommandData demolishUICommandData;
    CannonData data;
    public int Damage => data.damage;
    public float EnergyPerAttack => data.energyPerAttack;
    public float Range => data.range;
    public float MaxEnergy => data.maxEnergy;
    public float TimeBetweenAttacks => data.timeBetweenAttacks;
    float ElectricDevice.CurrentEnergy { get; set; }

    public bool IsOrderedToStop { get; set; }
    public bool IsOrderedToAttack { get; set; }

    public Damageable Target { get; set; }

    CannonStateFactory stateFactory;
    public CannonBaseState CurrentState { get; private set; }
    public bool IsSelected { get; private set; }
    public bool IsIdle { get; set; }
    
    GameObject rangeVisuals;

    protected override void Start() {
      UICommands = new List<UICommand>() { new DemolishUICommand(demolishUICommandData, this, storage) };
      base.Start();
      data = placedObjectType.customData as CannonData;
      rangeVisuals = transform.Find("Range").gameObject;
      rangeVisuals.transform.localScale = new Vector3(Range * 2, Range * 2, 1f);
      stateFactory = new CannonStateFactory(context: this);
      SwitchState(stateFactory.Idle);
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

    public void SwitchState(CannonBaseState state) {
      CurrentState = state;
      CurrentState.EnterState();
    }

    void Update() {
      CurrentState.UpdateState();
    }
  }
}
