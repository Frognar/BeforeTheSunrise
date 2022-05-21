using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace bts {
  public class Player : MonoBehaviour {
    public class OnSelectionEventArgs : EventArgs {
      public List<Selectable> Selected { get; }

      public OnSelectionEventArgs(List<Selectable> selected) {
        Selected = selected;
      }
    }

    public event EventHandler<OnSelectionEventArgs> OnSelection;

    PlayerInputs playerInputs;
    Camera cam;
    Vector3 startPosition;
    List<Selectable> selected;
    LineRenderer lineRenderer;

    void Awake() {
      playerInputs = FindObjectOfType<PlayerInputs>();
      lineRenderer = GetComponent<LineRenderer>();
      selected = new List<Selectable>();
      cam = Camera.main;
      lineRenderer.enabled = false;
    }

    void Update() {
      if (playerInputs.StartSelecting) {
        if (!EventSystem.current.IsPointerOverGameObject()) {
          StartSelectingArea(playerInputs.RayToWorld);
        }
      }

      if (lineRenderer.enabled) {
        DrawSelectionArea(playerInputs.RayToWorld);
        if (playerInputs.StopSelecting) {
          StopSelectingArea(playerInputs.ScreenPosition);
        }
      }
    }
    
    void StartSelectingArea(Ray ray) {
      lineRenderer.enabled = true;
      if (Physics.Raycast(ray, out RaycastHit hitInfo)) {
        startPosition = hitInfo.point;
      }
    }

    void DrawSelectionArea(Ray ray) {
      if (Physics.Raycast(ray, out RaycastHit hitInfo)) {
        Vector3 endPosition = hitInfo.point;
        Vector3 lowerLeft = new Vector3(
          Mathf.Min(startPosition.x, endPosition.x),
          1f,
          Mathf.Min(startPosition.z, endPosition.z));
        Vector3 upperRight = new Vector3(
          Mathf.Max(startPosition.x, endPosition.x),
          1f,
          Mathf.Max(startPosition.z, endPosition.z));
        lineRenderer.SetPositions(
          new Vector3[] {
              lowerLeft,
              new Vector3(lowerLeft.x, 1f, upperRight.z),
              upperRight,
              new Vector3(upperRight.x, 1f, lowerLeft.z),
          }
          );
      }
    }

    void StopSelectingArea(Vector2 screenPosition) {
      lineRenderer.enabled = false;
      DeselectAll();
      IEnumerable<Collider> selectedColliders = GetCollidersUnderSelectionArea(screenPosition);
      selected = FilterSelectable(selectedColliders).ToList();
      Select();
    }
    
    void DeselectAll() {
      foreach (Selectable selectable in selected) {
        selectable.Deselect();
      }

      selected.Clear();
    }
    
    IEnumerable<Collider> GetCollidersUnderSelectionArea(Vector2 screenPosition) {
      (Vector3 center, Vector3 halfExtents) = GetSelectionArea(screenPosition);
      return Physics.OverlapBox(center, halfExtents);
    }
    
    (Vector3 center, Vector3 halfExtents) GetSelectionArea(Vector2 screenPosition) {
      Ray ray = cam.ScreenPointToRay(screenPosition);
      if (Physics.Raycast(ray, out RaycastHit hitInfo)) {
        Vector3 endPoint = hitInfo.point;
        Vector3 center = (startPosition + endPoint) / 2f;
        Vector3 halfExtents = new Vector3(Mathf.Abs(startPosition.x - endPoint.x) / 2, 2, Mathf.Abs(startPosition.z - endPoint.z) / 2);
        return (center, halfExtents);
      }

      return (Vector3.zero, Vector3.zero);
    }

    IEnumerable<Selectable> FilterSelectable(IEnumerable<Collider> colliders) {
      IEnumerable<Selectable> selectables = GetSelectables(colliders);
      if (selectables.Any()) {
        IEnumerable<Selectable> playerSelectables = selectables.Where(s => s.ObjectAffiliation == Affiliation.Player);
        if (playerSelectables.Any()) {
          IEnumerable<Selectable> playerUnits = playerSelectables.Where(s => s.ObjectType == Type.Unit);
          return playerUnits.Any() ? playerUnits : new List<Selectable> { selectables.First() };
        }
        else {
          return new List<Selectable> { selectables.First() };
        }
      }

      return Enumerable.Empty<Selectable>();
    }
    
    IEnumerable<Selectable> GetSelectables(IEnumerable<Collider> colliders) {
      foreach (Collider collider in colliders) {
        if (collider.TryGetComponent(out Selectable selectable)) {
          yield return selectable;
        }
      }
    }

    void Select() {
      foreach (Selectable selectable in selected) {
        selectable.Select();
      }

      OnSelection?.Invoke(this, new OnSelectionEventArgs(selected));
    }

    public void Deselect(Selectable selectedObject) {
      _ = selected.Remove(selectedObject);
      selectedObject.Deselect();
      OnSelection?.Invoke(this, new OnSelectionEventArgs(selected));
    }
  }
}
