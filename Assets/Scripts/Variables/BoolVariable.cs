using UnityEngine;

namespace bts {
  [CreateAssetMenu(fileName = "boolVariable", menuName = "Variables/BoolVariable")]
  public class BoolVariable : ScriptableObject {
    [SerializeField] bool value;
    public bool Value { get => value; set => this.value = value; }
    public static implicit operator bool(BoolVariable b) => b.Value;
  }
}
