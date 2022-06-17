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
      const string file = "data.json";
      if (File.Exists(file)) {
        enemyDataForNights = JsonUtility.FromJson<EnemyDataForNights>(File.ReadAllText(file));
      }
      else {
        enemyDataForNights = new EnemyDataForNights(
          new List<DataModifier> {
            new DataModifier(1.5f, 15.0f),
            new DataModifier(1.65f, 16.5f),
            new DataModifier(1.8f, 18.0f),
            new DataModifier(1.95f, 19.5f),
            new DataModifier(2.1f, 21.0f),
            new DataModifier(2.25f, 22.5f),
            new DataModifier(2.5f, 25.0f),
            new DataModifier(2.64f, 26.35f),
            new DataModifier(2.77f, 27.7f),
            new DataModifier(2.9f, 29.05f),
            new DataModifier(3.04f, 30.4f),
            new DataModifier(3.18f, 31.75f),
            new DataModifier(3.31f, 33.1f),
            new DataModifier(3.45f, 34.45f),
            new DataModifier(3.58f, 35.8f),
            new DataModifier(3.72f, 37.15f),
            new DataModifier(3.85f, 38.5f),
            new DataModifier(3.99f, 39.85f),
            new DataModifier(4.12f, 41.2f),
            new DataModifier(4.26f, 42.55f),
            new DataModifier(4.39f, 43.9f),
            new DataModifier(4.53f, 45.25f),
            new DataModifier(4.66f, 46.6f),
            new DataModifier(4.8f, 47.95f),
            new DataModifier(5.0f, 50.0f),
            new DataModifier(5.2f, 52.0f),
            new DataModifier(5.4f, 54.0f),
            new DataModifier(5.6f, 56.0f),
            new DataModifier(5.8f, 58.0f),
            new DataModifier(6.0f, 60.0f)
          });
        File.WriteAllText(file, JsonUtility.ToJson(enemyDataForNights));
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
