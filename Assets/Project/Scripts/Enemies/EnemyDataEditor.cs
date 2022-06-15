using System;
using System.Collections.Generic;
using System.IO;
using fro.ValueAssets;
using UnityEngine;

namespace bts {
  public class EnemyDataEditor : MonoBehaviour {
    [SerializeField] EnemyData enemyData;
    [SerializeField] VoidEventChannel dayStarted;
    [SerializeField] IntAsset dayCounter;
    [SerializeField] EnemyDataForNights enemyDataForNights;
    [SerializeField] DataModifier endingModifier;

    void Awake() {
      if (File.Exists("data.json")) {
        enemyDataForNights = JsonUtility.FromJson<EnemyDataForNights>(File.ReadAllText("data.json"));
      }
      else {
        File.Create("data.json");
      }
    }
    
    void OnEnable() {
      dayStarted.OnEventInvoked += OnNewDay;
    }

    void OnDisable() {
      dayStarted.OnEventInvoked -= OnNewDay;
    }

    void OnNewDay(object sender, EventArgs e) {
      if (enemyDataForNights.DataModifiers.Count < dayCounter) {
        enemyData.Damage = enemyDataForNights.DataModifiers[dayCounter].Damage;
        enemyData.MaxHealth = enemyDataForNights.DataModifiers[dayCounter].Health;
      }
      else {
        enemyData.Damage += endingModifier.Damage;
        enemyData.MaxHealth += endingModifier.Health;
      }
    }

    [Serializable]
    public class EnemyDataForNights {
      public List<DataModifier> DataModifiers;

      public EnemyDataForNights(List<DataModifier> dataModifiers) {
        DataModifiers = dataModifiers;
      }
    }

    [Serializable]
    public class DataModifier {
      public float Damage;
      public float Health;

      public DataModifier(float damage, float health) {
        Damage = damage;
        Health = health;
      }
    }
  }
}
