namespace bts {
  public class PopupTextConfiguration : PooledObjectConfiguration<PopupText> {
    public static PopupTextConfiguration Default = CreateInstance<PopupTextConfiguration>();
    PopupTextConfiguration() { }

    public override void ApplyTo(PopupText obj) {
      const float duration = 1f;
      obj.ReleaseAfter(duration);
    }
  }
}
