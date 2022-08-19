using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public static PathFinding Instance { get; private set; }

    [SerializeField] private Transform debuggerPrefab;

    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    private GridSystem<PathNode> gridSystem;

    private void Awake() 
    {
        if(Instance != null)
        {
            Debug.LogError($"More than one instance for {this}");
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start() 
    {
        gridSystem = new GridSystem<PathNode>(10, 10, 2, 
                (GridSystem<PathNode> gridSystem, GridPosition gridPosition) => new PathNode(gridPosition));
        gridSystem.CreateGridDebugObject(debuggerPrefab);
    }

    public List<GridPosition> FindPath(GridPosition startPosition, GridPosition endPosition)
    {
        List<PathNode> openNodeList = new List<PathNode>();
        List<PathNode> closedNodeList = new List<PathNode>();

        PathNode startNode = gridSystem.GetGridObject(startPosition);
        PathNode endNode = gridSystem.GetGridObject(endPosition);

        openNodeList.Add(startNode);

        for (int x = 0; x < gridSystem.GetWidth(); x++)
        {
            for (int z = 0; z < gridSystem.GetHeight(); z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                PathNode pathNode = gridSystem.GetGridObject(gridPosition);

                pathNode.SetGCost(int.MaxValue);
                pathNode.SetHCost(0);
                pathNode.CalculateFCost();
                pathNode.ResetPreviousPathNode();
            }
        }

        startNode.SetGCost(0);
        startNode.SetHCost(CalculateDistance(startPosition, endPosition));
        startNode.CalculateFCost();

        while(openNodeList.Count > 0)
        {
            PathNode currentPathNode = GetLowerFCostPathNode(openNodeList);

            if(currentPathNode == endNode)
            {
                //Path Found
                return CalculatedPath(endNode);
            }

            openNodeList.Remove(currentPathNode);
            closedNodeList.Add(currentPathNode);

            foreach(PathNode neighbourPathNode in GetNeighbourPathNodeList(currentPathNode))
            {
                if(closedNodeList.Contains(neighbourPathNode)) continue;

                int calculatedGCost = currentPathNode.GetGCost() + 
                                CalculateDistance(currentPathNode.GetGridPosition(), neighbourPathNode.GetGridPosition());

                if(calculatedGCost < neighbourPathNode.GetGCost())
                {
                    neighbourPathNode.SetPreviousNode(currentPathNode);
                    neighbourPathNode.SetGCost(calculatedGCost);
                    neighbourPathNode.SetHCost(CalculateDistance(neighbourPathNode.GetGridPosition(), endPosition));
                    neighbourPathNode.CalculateFCost();
    
                    if(!openNodeList.Contains(neighbourPathNode))
                    {
                        openNodeList.Add(neighbourPathNode);
                    }
                }

            }
        }

        return null;
    }

    private int CalculateDistance(GridPosition gridPositionA, GridPosition gridPositionB)
    {
        GridPosition gridDistance = gridPositionA - gridPositionB;
        int distance = Mathf.Abs(gridDistance.x) + Mathf.Abs(gridDistance.z);

        int xDistance = Mathf.Abs(gridDistance.x);
        int zDistance = Mathf.Abs(gridDistance.z);

        int remainingDistance = Mathf.Abs(xDistance - zDistance);

        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, zDistance) + MOVE_STRAIGHT_COST * remainingDistance;
    }

    private PathNode GetLowerFCostPathNode(List<PathNode> pathNodeList)
    {
        PathNode lowerFCostPathNode = pathNodeList[0];

        for (int i = 0; i < pathNodeList.Count; i++)
        {
            if(pathNodeList[i].GetFCost() < lowerFCostPathNode.GetFCost())
            {
                lowerFCostPathNode = pathNodeList[i];
            }
        }

        return lowerFCostPathNode;
    }

    private PathNode GetPathNode(int x, int z)
    {
        return gridSystem.GetGridObject(new GridPosition(x, z));
    }

    private List<GridPosition> CalculatedPath(PathNode endNode)
    {
        List<PathNode> pathList = new List<PathNode>();

        PathNode curentNode = endNode;

        while(curentNode.GetPreviousPathNode() != null)
        {
            pathList.Add(curentNode.GetPreviousPathNode());
            curentNode = curentNode.GetPreviousPathNode();
        }

        pathList.Reverse();
        List<GridPosition> pathGridPositionList = new List<GridPosition>();

        foreach(PathNode pathNode in pathList)
        {
            pathGridPositionList.Add(pathNode.GetGridPosition());
        }

        return pathGridPositionList;
    }

    private List<PathNode> GetNeighbourPathNodeList(PathNode currentNode)
    {
        List<PathNode> neighbourPathNodeList = new List<PathNode>();

        GridPosition currentGridPosition = currentNode.GetGridPosition();

        if(currentGridPosition.x + 1 < gridSystem.GetWidth())
        {
            neighbourPathNodeList.Add(GetPathNode(currentGridPosition.x + 1, currentGridPosition.z));

            if(currentGridPosition.z + 1 < gridSystem.GetHeight())
            {
                neighbourPathNodeList.Add(GetPathNode(currentGridPosition.x + 1, currentGridPosition.z + 1));
            }

            if(currentGridPosition.z - 1 >= 0)
            {
                neighbourPathNodeList.Add(GetPathNode(currentGridPosition.x + 1, currentGridPosition.z - 1));
            }
        }

        if(currentGridPosition.x - 1 >= 0)
        {
            neighbourPathNodeList.Add(GetPathNode(currentGridPosition.x - 1, currentGridPosition.z));

            if(currentGridPosition.z + 1 < gridSystem.GetWidth())
            {
                neighbourPathNodeList.Add(GetPathNode(currentGridPosition.x - 1, currentGridPosition.z + 1));
            }

            if(currentGridPosition.z - 1 <= 0)
            {
                neighbourPathNodeList.Add(GetPathNode(currentGridPosition.x - 1, currentGridPosition.z - 1));
            }
        }

        if(currentGridPosition.z + 1 < gridSystem.GetHeight())
        {
            neighbourPathNodeList.Add(GetPathNode(currentGridPosition.x, currentGridPosition.z + 1));
        }

        if(currentGridPosition.z - 1 >= 0)
        {
            neighbourPathNodeList.Add(GetPathNode(currentGridPosition.x, currentGridPosition.z - 1));        
        }

        return neighbourPathNodeList;
    }
}
