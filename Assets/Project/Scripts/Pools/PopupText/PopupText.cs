using TMPro;
using UnityEngine;
using UnityEngine.Pool;

namespace bts {
  public class PopupText : MonoBehaviour, Poolable {
    [SerializeField] Color[] colors = new Color[5];
    [field: SerializeField] public TMP_Text Text { get; private set; }
    IObjectPool<PopupText> popupTextPool;

    public void SetGemstone(GemstoneType type) {
      Text.color = colors[(int)Mathf.Log((int)type, 2)];
    }

    public void ReleaseAfter(float duration) {
      Invoke(nameof(Release), duration);
    }

    void Release() {
      popupTextPool.Release(this);
    }
    
    void Update() {
      transform.position += Vector3.up * (5f * Time.deltaTime);
    }

    void LateUpdate() {
      transform.LookAt(Camera.main.transform);
    }

    public void SetPool<T>(IObjectPool<T> pool) where T : MonoBehaviour, Poolable {
      popupTextPool = pool as IObjectPool<PopupText>;
    }
  }
}
