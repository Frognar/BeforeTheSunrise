using System;
using UnityEngine;

namespace fro.BuildingSystem {
  public class GridXZ<T> where T : class {
    public event EventHandler<GridObjectChangedEventArgs> OnGridObjectChanged;

    public class GridObjectChangedEventArgs : EventArgs {
      public GridCords Cords { get; }

      public GridObjectChangedEventArgs(GridCords cords) {
        Cords = cords;
      }
    }

    public int Width { get; private set; }
    public int Height { get; private set; }
    float CellSize { get; }
    Vector3 OriginPosition { get; }
    T[,] Grid2D { get; }

    public GridXZ(int width, int height, float cellSize, Vector3 originPosition, Func<GridXZ<T>, GridCords, T> createGridObject) {
      Width = width;
      Height = height;
      CellSize = cellSize;
      OriginPosition = originPosition;
      Grid2D = new T[Width, Height];
      for (int x = 0; x < Grid2D.GetLength(0); x++) {
        for (int z = 0; z < Grid2D.GetLength(1); z++) {
          Grid2D[x, z] = createGridObject(this, new GridCords(x, z));
        }
      }
    }

    public void SetGridObject(Vector3 worldPosition, T value) {
      SetGridObject(GetCords(worldPosition), value);
    }
    
    public void SetGridObject(GridCords cords, T value) {
      if (CordsAreValid(cords)) {
        Grid2D[cords.X, cords.Z] = value;
        OnGridObjectChanged.Invoke(this, new GridObjectChangedEventArgs(cords));
      }
    }

    public void TriggerOnGridObjectChanged(GridCords cords) {
      OnGridObjectChanged?.Invoke(this, new GridObjectChangedEventArgs(cords));
    }

    public T GetGridObject(Vector3 worldPosition) {
      return GetGridObject(GetCords(worldPosition));
    }
    
    public T GetGridObject(GridCords cords) {
      return CordsAreValid(cords) ? Grid2D[cords.X, cords.Z] : null;
    }
    
    bool CordsAreValid(GridCords cords) {
      return cords.X >= 0 && cords.X < Width
          && cords.Z >= 0 && cords.Z < Height;
    }

    public Vector3 GetWorldPosition(GridCords cords) {
      return new Vector3(cords.X, 0f, cords.Z) * CellSize + OriginPosition;
    }

    public GridCords GetCords(Vector3 worldPosition) {
      int x = Mathf.FloorToInt((worldPosition - OriginPosition).x / CellSize);
      int z = Mathf.FloorToInt((worldPosition - OriginPosition).z / CellSize);
      return new GridCords(x, z);
    }
  }
}
