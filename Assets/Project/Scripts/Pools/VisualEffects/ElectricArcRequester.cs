using UnityEngine;

namespace bts {
  public class ElectricArcRequester : MonoBehaviour {
    [SerializeField] ElectricArcVFXConfiguration configuration;
    [SerializeField] ElectricArcEventChannel channel;
    [SerializeField] Transform arcBegin;
    readonly ElectricArcParameters parameters = new ElectricArcParameters();

    public void Create(Vector3 targetPosition) {
      parameters.Source = arcBegin;
      parameters.TargetPosition = targetPosition;
      channel.RaiseSpawnEvent(configuration, parameters);
    }
  }
}
