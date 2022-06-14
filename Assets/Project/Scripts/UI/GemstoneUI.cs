using bts.Gemstones;
using TMPro;
using UnityEngine;

namespace bts {
  public class GemstoneUI : MonoBehaviour {
    [SerializeField] TMP_Text sapphireText;
    [SerializeField] TMP_Text emeraldText;
    [SerializeField] TMP_Text rubbyText;
    [SerializeField] TMP_Text amethystText;
    [SerializeField] TMP_Text topazText;
    [SerializeField] GemstoneStorage storage;

    void Start() {
      UpdateUI();
    }

    void UpdateUI() {
      sapphireText.text = storage.Gemstones[GemstoneType.Sapphire].ToString();
      emeraldText.text = storage.Gemstones[GemstoneType.Emerald].ToString();
      rubbyText.text = storage.Gemstones[GemstoneType.Rubby].ToString();
      amethystText.text = storage.Gemstones[GemstoneType.Amethyst].ToString();
      topazText.text = storage.Gemstones[GemstoneType.Topaz].ToString();
    }

    void OnEnable() {
      storage.StorageChanged += OnStorageChanged;
    }

    void OnDisable() {
      storage.StorageChanged -= OnStorageChanged;
    }

    void OnStorageChanged(object sender, System.EventArgs e) {
      UpdateUI();
    }
  }
}
