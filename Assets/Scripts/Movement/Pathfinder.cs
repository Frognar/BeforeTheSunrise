using System.Collections.Generic;
using System.Linq;
using Pathfinding;
using UnityEngine;

namespace bts {
  public class Pathfinder : MonoBehaviour {
    AIPath aiPath;
    AIDestinationSetter aiDestinationSetter;
    float defaultEndReachedDistance;

    void Awake() {
      aiPath = GetComponent<AIPath>();
      aiDestinationSetter = GetComponent<AIDestinationSetter>();
      defaultEndReachedDistance = aiPath.endReachedDistance;
    }

    public void SetDestination(Vector3 destination) {
      aiDestinationSetter.target = null;
      aiPath.destination = destination;
    }

    public void SetTarget(Transform target) {
      aiDestinationSetter.target = target;
    }

    public void SetStopDistance(float stopDistance) {
      aiPath.endReachedDistance = stopDistance;
    }

    public void ResetStopDistance() {
      aiPath.endReachedDistance = defaultEndReachedDistance;
    }

    public void Reset() {
      aiDestinationSetter.target = null;
      aiPath.destination = transform.position;
      ResetStopDistance();
    }

    public bool IsPathPossible(Bounds bounds) {
      GraphNode myNode = (AstarPath.active.graphs[0] as GridGraph).GetNearest(transform.position).node;
      bounds.size += Vector3.one * 2f;
      List<GraphNode> targetNodes = (AstarPath.active.graphs[0] as GridGraph).GetNodesInRegion(bounds);
      return targetNodes.Any(targetNode => PathUtilities.IsPathPossible(myNode, targetNode));
    }

    public bool IsPathPossible(Vector3 target) {
      GraphNode myNode = (AstarPath.active.graphs[0] as GridGraph).GetNearest(transform.position).node;
      GraphNode targetNode = (AstarPath.active.graphs[0] as GridGraph).GetNearest(target).node;
      return PathUtilities.IsPathPossible(myNode, targetNode);
    }
  }
}
