using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace bts {
  public class Player : MonoBehaviour {
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
      if (playerInputs.IsLeftBtnDawn) {
        StartSelectingArea();
      }

      if (lineRenderer.enabled) {
        DrawSelectionArea();
      }

      if (playerInputs.IsLeftBtnUp) {
        StopSelectingArea();
      }

      if (playerInputs.IsRightBtnDawn) {
        foreach (Selectable selectable in selected) {
          if (selectable.Transform.TryGetComponent(out Unit unit)) {
            Ray ray = cam.ScreenPointToRay(playerInputs.MouseScreenPosition);
            unit.Execute(ray);
          }
        }
      }
    }
    
    void StartSelectingArea() {
      lineRenderer.enabled = true;
      Ray ray = cam.ScreenPointToRay(playerInputs.MouseScreenPosition);
      if (Physics.Raycast(ray, out RaycastHit hitInfo)) {
        startPosition = hitInfo.point;
      }
    }

    void DrawSelectionArea() {
      Ray ray = cam.ScreenPointToRay(playerInputs.MouseScreenPosition);
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

    void StopSelectingArea() {
      lineRenderer.enabled = false;
      DeselectAll();
      IEnumerable<Collider> selectedColliders = GetCollidersUnderSelectionArea();
      selected = FilterSelectable(selectedColliders).ToList();
      Select();
    }
    
    void DeselectAll() {
      foreach (Selectable selectable in selected) {
        selectable.Deselect();
      }

      selected.Clear();
    }
    
    IEnumerable<Collider> GetCollidersUnderSelectionArea() {
      (Vector3 center, Vector3 halfExtents) = GetSelectionArea();
      return Physics.OverlapBox(center, halfExtents);
    }
    
    (Vector3 center, Vector3 halfExtents) GetSelectionArea() {
      Ray ray = cam.ScreenPointToRay(playerInputs.MouseScreenPosition);
      if (Physics.Raycast(ray, out RaycastHit hitInfo)) {
        Vector3 endPosition = hitInfo.point;
        Vector3 center = (startPosition + endPosition) / 2f;
        Vector3 halfExtents = new Vector3(Mathf.Abs(startPosition.x - endPosition.x) / 2, 2, Mathf.Abs(startPosition.z - endPosition.z) / 2);
        return (center, halfExtents);
      }

      return (Vector3.zero, Vector3.zero);
    }

    IEnumerable<Selectable> FilterSelectable(IEnumerable<Collider> colliders) {
      IEnumerable<Selectable> selectables = GetSelectables(colliders);
      if (selectables.Any()) {
        IEnumerable<Selectable> playerSelectables = selectables.Where(s => s.ObjectAffiliation == Selectable.Affiliation.Player);
        if (playerSelectables.Any()) {
          IEnumerable<Selectable> playerUnits = playerSelectables.Where(s => s.ObjectType == Selectable.Type.Unit);
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
    }
  }
}
