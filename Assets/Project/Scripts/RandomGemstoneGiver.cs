using UnityEngine;

namespace bts.Gemstones {
  public class RandomGemstoneGiver : MonoBehaviour {
    [SerializeField] PopupTextEventChannel popupTextEventChannel;
    [SerializeField] GemstoneStorage storage;
    [SerializeField][Range(0, 1)] float addResourceChance = 0.5f;
    [SerializeField][Range(0, 2)] int minValue;
    [SerializeField][Range(2, 6)] int maxValue;
    [SerializeField] Transform center;

    public void Give() {
      if (Random.value < addResourceChance) {
        GemstoneType type = storage.GetRandomType();
        int count = Random.Range(minValue, maxValue + 1);
        storage.Store(type, count);
        PopupTextParameters popupParams = new PopupTextParameters() {
          Position = center.position,
          GemstoneType = type,
          Text = $"+{count}"
        };

        popupTextEventChannel.RaiseSpawnEvent(PopupTextConfiguration.Default, popupParams);
      }
    }
  }
}
