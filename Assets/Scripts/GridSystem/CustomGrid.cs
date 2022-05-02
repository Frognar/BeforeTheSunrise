using System;
using UnityEngine;

namespace bts {
  public class CustomGrid<T> where T : class {
    public event EventHandler<GridObjectChangedEventArgs> OnGridObjectChanged;

    public class GridObjectChangedEventArgs : EventArgs {
      public int x;
      public int y;

      public GridObjectChangedEventArgs(int x, int y) {
        this.x = x;
        this.y = y;
      }
    }

    int Width { get; }
    int Height { get; }
    float CellSize { get; }
    Vector3 OriginPosition { get; }
    T[,] Grid2D { get; }

    public CustomGrid(int width, int height, float cellSize, Vector3 originPosition, Func<CustomGrid<T>, int, int, T> createGridObject) {
      Width = width;
      Height = height;
      CellSize = cellSize;
      OriginPosition = originPosition;
      Grid2D = new T[Width, Height];

      Color color = Color.white;
      for (int x = 0; x < Grid2D.GetLength(0); x++) {
        for (int y = 0; y < Grid2D.GetLength(1); y++) {
          Grid2D[x, y] = createGridObject(this, x, y);
          Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), color, 100f);
          Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), color, 100f);
        }
      }
      
      Debug.DrawLine(GetWorldPosition(0, Height), GetWorldPosition(Width, Height), color, 100f);
      Debug.DrawLine(GetWorldPosition(Width, 0), GetWorldPosition(Width, Height), color, 100f);
    }

    public void SetGridObject(int x, int y, T value) {
      if (CordsAreValid(x, y)) {
        Grid2D[x, y] = value;
        OnGridObjectChanged?.Invoke(this, new GridObjectChangedEventArgs(x, y));
      }
    }

    public void TriggerOnGridObjectChanged(int x, int y) {
        OnGridObjectChanged?.Invoke(this, new GridObjectChangedEventArgs(x, y));
    }

    public void SetGridObject(Vector3 worldPosition, T value) {
      Vector2Int cords = GetCords(worldPosition);
      SetGridObject(cords.x, cords.y, value);
    }

    public T GetGridObject(int x, int y) {
      return CordsAreValid(x, y) ? Grid2D[x, y] : default;
    }

    public T GetGridObject(Vector3 worldPosition) {
      Vector2Int cords = GetCords(worldPosition);
      return GetGridObject(cords.x, cords.y);
    }

    bool CordsAreValid(int x, int y) {
      return x >= 0
          && y >= 0
          && x < Width
          && y < Height;
    }

    public Vector3 GetWorldPosition(int x, int y) {
      return new Vector3(x, 0f, y) * CellSize + OriginPosition;
    }

    public Vector2Int GetCords(Vector3 worldPosition) {
      int x = Mathf.CeilToInt((worldPosition - OriginPosition).x / CellSize);
      int y = Mathf.CeilToInt((worldPosition - OriginPosition).y / CellSize);
      return new Vector2Int(x, y);
    }
  }
}
