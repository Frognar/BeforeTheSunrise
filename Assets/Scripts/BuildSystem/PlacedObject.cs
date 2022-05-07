﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bts {
  public class PlacedObject : MonoBehaviour, Selectable, Damageable {
    static readonly WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
    static Transform parent;
    
    public static PlacedObject Create(Vector3 worldPosition, Vector3Int origin, PlacedObjectTypeSO placedObjectType, GridBuildingSystem gridBuildingSystem) {
      if (parent == null) {
        parent = new GameObject("Buildings|Obstacles").transform;
      }

      Transform placedObjectTransform = Instantiate(placedObjectType.prefab, worldPosition, Quaternion.identity, parent);
      PlacedObject placedObject = placedObjectTransform.GetComponent<PlacedObject>();
      placedObject.placedObjectType = placedObjectType;
      placedObject.origin = origin;
      placedObject.gridBuildingSystem = gridBuildingSystem;
      return placedObject;
    }

    public string Name => placedObjectType.name;
    public Transform Transform => transform;
    public Affiliation ObjectAffiliation => placedObjectType.objectAffiliation;
    public Type ObjectType => placedObjectType.objectType;
    public GameObject Selected { get; private set; }

    Health health;
    WorldHealthBar bar;
    Collider obstacle;

    PlacedObjectTypeSO placedObjectType;
    GridBuildingSystem gridBuildingSystem;
    Vector3Int origin;

    void Awake() {
      obstacle = GetComponent<Collider>();
      health = new Health(10);
      bar = GetComponentInChildren<WorldHealthBar>();
      Selected = transform.Find("Selected").gameObject;
    }

    void Start() {
      obstacle.enabled = true;
      AstarPath.active.UpdateGraphs(obstacle.bounds);
      bar.SetUp(health);
    }

    public List<Vector3Int> GetGridPositions() {
      return placedObjectType.GetGridPositions(origin);
    }

    public void TakeDamage(int amount) {
      health.Damage(amount);
      if (health.CurrentHealth == 0) {
        gridBuildingSystem.Demolish(transform.position);
      }
    }

    public void DestroySelf() {
      _ = StartCoroutine(DestroySelfAfterDelay());
    }

    IEnumerator DestroySelfAfterDelay() {
      yield return waitForFixedUpdate;
      obstacle.enabled = false;
      yield return waitForFixedUpdate;
      AstarPath.active.UpdateGraphs(obstacle.bounds);
      yield return waitForFixedUpdate;
      yield return waitForFixedUpdate;
      obstacle.enabled = true;
      yield return waitForFixedUpdate;
      gameObject.transform.position = new Vector3(-10000, -10000, -10000);
      yield return waitForFixedUpdate;
      Destroy(gameObject);
    }
  }
}