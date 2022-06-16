using System;
using System.Collections.Generic;
using System.IO;
using fro.ValueAssets;
using UnityEngine;

namespace bts {
  public class GameLogger : MonoBehaviour {
    [SerializeField] VoidEventChannel onDayStarted;
    [SerializeField] VoidEventChannel onDeath;
    [SerializeField] StorageLogger storageLogger;
    [SerializeField] BuildingLoger buildingLogger;
    [SerializeField] UpgradeLogger updateLogger;
    [SerializeField] IntAsset dayCounter;
    
    void OnEnable() {
      onDayStarted.OnEventInvoked += LogData;
      onDeath.OnEventInvoked += LogData;
    }

    void OnDisable() {
      onDayStarted.OnEventInvoked -= LogData;
      onDeath.OnEventInvoked -= LogData;
    }

    void LogData(object sender, EventArgs e) {
      const string logDir = "logs";
      if (Directory.Exists(logDir) == false) {
        _ = Directory.CreateDirectory(logDir);
      }
      
      string logFile = Path.Combine(logDir, $"{DateTime.Today:yyyy-MM-dd}.log");
      if (File.Exists(logFile)) {
        File.AppendAllLines(logFile, GetLogData());
      }
      else {
        File.WriteAllLines(logFile, GetLogData());
      }
    }

    IEnumerable<string> GetLogData() {
      yield return $"Day: {dayCounter.value}";
      yield return storageLogger.GetLogData();
      yield return buildingLogger.GetLogData();
      yield return updateLogger.GetLogData();
    }
  }
}
