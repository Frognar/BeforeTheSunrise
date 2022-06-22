using fro.ValueAssets;

namespace bts {
  public interface Limited {
    int Limit { get; }
    IntAsset PlacedCount { get; }
    bool CanPlace() {
      return PlacedCount < Limit;
    }
  }
}
