using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

namespace bts {
  [VFXBinder("Transform/Transform")]
  public class CustomVFXTransformBinder : VFXBinderBase {
    public string Property { get => (string)property; set { property = value; UpdateSubProperties(); } }

    [VFXPropertyBinding("UnityEditor.VFX.Transform"), SerializeField, UnityEngine.Serialization.FormerlySerializedAs("m_Parameter")]
    protected ExposedProperty property = "Transform";
    public Transform Target = null;

    ExposedProperty position;
    ExposedProperty angles;
    ExposedProperty scale;
    
    protected override void OnEnable() {
      base.OnEnable();
      UpdateSubProperties();
    }

    void OnValidate() {
      UpdateSubProperties();
    }

    void UpdateSubProperties() {
      position = property + "_position";
      angles = property + "_angles";
      scale = property + "_scale";
    }

    public override bool IsValid(VisualEffect component) {
      return Target != null && component.HasVector3((int)position) && component.HasVector3((int)angles) && component.HasVector3((int)scale);
    }

    public override void UpdateBinding(VisualEffect component) {
      component.SetVector3((int)position, Target.position);
      component.SetVector3((int)angles, Target.eulerAngles);
      component.SetVector3((int)scale, Target.localScale);
    }

    public override string ToString() {
      return string.Format("Transform : '{0}' -> {1}", property, Target == null ? "(null)" : Target.name);
    }
  }
}
