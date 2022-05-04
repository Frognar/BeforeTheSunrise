using System;
using UnityEngine;

namespace bts {
  public class GridXZ<T> where T : class {
    public event EventHandler<GridObjectChangedEventArgs> OnGridObjectChanged;

    public class GridObjectChangedEventArgs : EventArgs {
      public int X { get; }
      public int Z { get; }

      public GridObjectChangedEventArgs(int x, int z) {
        X = x;
        Z = z;
      }
    }

    int Width { get; }
    int Height { get; }
    float CellSize { get; }
    Vector3 OriginPosition { get; }
    T[,] Grid2D { get; }

    public GridXZ(int width, int height, float cellSize, Vector3 originPosition, Func<GridXZ<T>, int, int, T> createGridObject) {
      Width = width;
      Height = height;
      CellSize = cellSize;
      OriginPosition = originPosition;
      Grid2D = new T[Width, Height];

      Color color = Color.white;
      for (int x = 0; x < Grid2D.GetLength(0); x++) {
        for (int z = 0; z < Grid2D.GetLength(1); z++) {
          Grid2D[x, z] = createGridObject(this, x, z);
          Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x + 1, z), color, 100f);
          Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z + 1), color, 100f);
        }
      }
      
      Debug.DrawLine(GetWorldPosition(0, Height), GetWorldPosition(Width, Height), color, 100f);
      Debug.DrawLine(GetWorldPosition(Width, 0), GetWorldPosition(Width, Height), color, 100f);
    }

    public void SetGridObject(int x, int z, T value) {
      if (CordsAreValid(x, z)) {
        Grid2D[x, z] = value;
        OnGridObjectChanged?.Invoke(this, new GridObjectChangedEventArgs(x, z));
      }
    }

    public void TriggerOnGridObjectChanged(int x, int z) {
        OnGridObjectChanged?.Invoke(this, new GridObjectChangedEventArgs(x, z));
    }

    public void SetGridObject(Vector3 worldPosition, T value) {
      Vector3Int cords = GetCords(worldPosition);
      SetGridObject(cords.x, cords.z, value);
    }

    public T GetGridObject(int x, int z) {
      return CordsAreValid(x, z) ? Grid2D[x, z] : default;
    }

    public T GetGridObject(Vector3 worldPosition) {
      Vector3Int cords = GetCords(worldPosition);
      return GetGridObject(cords.x, cords.z);
    }

    bool CordsAreValid(int x, int z) {
      return x >= 0
          && z >= 0
          && x < Width
          && z < Height;
    }

    public Vector3 GetWorldPosition(Vector3Int cords) {
      return GetWorldPosition(cords.x, cords.z);
    }

    public Vector3 GetWorldPosition(int x, int z) {
      return new Vector3(x, 0f, z) * CellSize + OriginPosition;
    }

    public Vector3Int GetCords(Vector3 worldPosition) {
      int x = Mathf.CeilToInt((worldPosition - OriginPosition).x / CellSize);
      int z = Mathf.CeilToInt((worldPosition - OriginPosition).z / CellSize);
      return new Vector3Int(x, 0, z);
    }
  }
}
