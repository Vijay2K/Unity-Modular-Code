using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private int gCost;
    private int hCost;
    private int fCost;
    private PathNode previousNode;
    private GridPosition gridPosition;

    public PathNode(GridPosition gridPosition)
    {
        this.gridPosition = gridPosition;
    }

    public void SetGCost(int gCost)
    {
        this.gCost = gCost;
    }        

    public void SetHCost(int hCost)
    {
        this.hCost = hCost;
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    public void SetPreviousNode(PathNode previousNode)
    {
        this.previousNode = previousNode;
    }

    public void ResetPreviousPathNode()
    {
        previousNode = null;
    }

    public int GetGCost() => gCost;
    public int GetHCost() => hCost;
    public int GetFCost() => fCost;
    public PathNode GetPreviousPathNode() => previousNode;
    public GridPosition GetGridPosition() => gridPosition;

    public override string ToString()
    {
        return $"({gridPosition.x}, {gridPosition.z})";
    }
}
