using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace bts {
  public class Player : MonoBehaviour {
    [SerializeField] InputReader inputReader;
    [SerializeField] SelectablesEventChannel selectablesEventChannel;
    LineRenderer lineRenderer;
    List<Selectable> selected;
    Vector3 startPosition;
    Vector3 halfExtents = new Vector3(25f, 5f, 25f);
    bool isMouseOverUI;

    void Awake() {
      lineRenderer = GetComponent<LineRenderer>();
      selected = new List<Selectable>();
      lineRenderer.enabled = false;
    }

    void Update() {
      isMouseOverUI = EventSystem.current.IsPointerOverGameObject();
    }

    void OnEnable() {
      selectablesEventChannel.OnDeselect += Deselect;
      inputReader.SelectSameEvent += SelectMany;
      inputReader.StartSelectiongEvent += StartSelectingArea;
      inputReader.WorldPositionEvent += DrawSelectionArea;
      inputReader.StopSelectiongEvent += StopSelectingArea;
    }

    void OnDisable() {
      selectablesEventChannel.OnDeselect -= Deselect;
      inputReader.SelectSameEvent -= SelectMany;
      inputReader.StartSelectiongEvent -= StartSelectingArea;
      inputReader.WorldPositionEvent -= DrawSelectionArea;
      inputReader.StopSelectiongEvent -= StopSelectingArea;
    }

    void Deselect(object sender, Selectable selectedObject) {
      _ = selected.Remove(selectedObject);
      selectedObject.Deselect();
      selectablesEventChannel.Invoke(selected);
    }

    private void SelectMany(Ray ray) {
      lineRenderer.enabled = false;
      if (Physics.Raycast(ray, out RaycastHit hitInfo)) {
        if (hitInfo.transform.TryGetComponent(out Selectable selectable)) {
          Deselect();
          if (selectable.ObjectAffiliation == Affiliation.Player) {
            selected = GetSelectablesInBox(hitInfo.point, halfExtents).Where(s => s.IsSameAs(selectable)).ToList();
          }
          else {
            selected = new List<Selectable> { selectable };
          }

          Select();
        }
      }
    }

    void Deselect() {
      foreach (Selectable selectable in selected) {
        selectable.Deselect();
      }

      selected.Clear();
    }

    IEnumerable<Selectable> GetSelectablesInBox(Vector3 center, Vector3 halfExtents) {
      Collider[] colliders = Physics.OverlapBox(center, halfExtents);
      return GetSelectables(colliders);
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

      selectablesEventChannel.Invoke(selected);
    }

    void StartSelectingArea(Vector3 position) {
      if (!isMouseOverUI) {
        lineRenderer.SetPositions(new Vector3[] { position, position, position, position });
        lineRenderer.enabled = true;
        startPosition = position;
      }
    }

    void DrawSelectionArea(Vector3 currentPosition) {
      if (lineRenderer.enabled) {
        Vector3[] selectionAreaCorners = GetSelectionCorners(currentPosition);
        lineRenderer.SetPositions(selectionAreaCorners);
      }
    }

    Vector3[] GetSelectionCorners(Vector3 endPosition) {
      Vector3 lowerLeft = new Vector3(Mathf.Min(startPosition.x, endPosition.x), 1f, Mathf.Min(startPosition.z, endPosition.z));
      Vector3 upperRight = new Vector3(Mathf.Max(startPosition.x, endPosition.x), 1f, Mathf.Max(startPosition.z, endPosition.z));
      return new Vector3[] { lowerLeft, new Vector3(lowerLeft.x, 1f, upperRight.z), upperRight, new Vector3(upperRight.x, 1f, lowerLeft.z) };
    }

    void StopSelectingArea(Vector3 endPoint) {
      if (lineRenderer.enabled) {
        lineRenderer.enabled = false;
        Deselect();
        (Vector3 center, Vector3 halfExtents) = GetSelectionArea(endPoint);
        IEnumerable<Selectable> selectables = GetSelectablesInBox(center, halfExtents);
        selected = FilterSelectable(selectables);
        Select();
      }
    }

    (Vector3 center, Vector3 halfExtents) GetSelectionArea(Vector3 endPoint) {
      Vector3 center = (startPosition + endPoint) / 2f;
      Vector3 halfExtents = new Vector3(Mathf.Abs(startPosition.x - endPoint.x) / 2, 2, Mathf.Abs(startPosition.z - endPoint.z) / 2);
      return (center, halfExtents);
    }

    List<Selectable> FilterSelectable(IEnumerable<Selectable> selectables) {
      if (selectables.Any()) {
        IEnumerable<Selectable> playerSelectables = selectables.Where(s => s.ObjectAffiliation == Affiliation.Player);
        if (playerSelectables.Any()) {
          IEnumerable<Selectable> playerUnits = playerSelectables.Where(s => s.ObjectType == Type.Unit);
          return playerUnits.Any() ? playerUnits.ToList() : playerSelectables.ToList();
        }
        else {
          return new List<Selectable> { selectables.First() };
        }
      }

      return new List<Selectable>();
    }
  }
}
