using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem
{
    private int width;
    private int height;
    private int cellSize;
    private GridObject[,] gridObjectsArray;

    public GridSystem(int width, int height, int cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridObjectsArray = new GridObject[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                gridObjectsArray[x, z] = new GridObject(this, gridPosition);
            }
        }
    }

    public Vector3 GetWorldPosition3D(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.x, 0, gridPosition.z) * cellSize;
    }

    public Vector2 GetWorldPosition2D(GridPosition gridPosition)
    {
        return new Vector2(gridPosition.x, gridPosition.z) * cellSize;
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return new GridPosition(Mathf.RoundToInt(worldPosition.x / cellSize), Mathf.RoundToInt(worldPosition.z / cellSize));
    }

    private GridObject GetGridObject(GridPosition gridPosition)
    {
        return gridObjectsArray[gridPosition.x, gridPosition.z];
    }

    public void CreateGridDebugObject(Transform gridDebuggerTransform, Transform spawnTransform)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                Transform gridDebuggerTransformInstance = GameObject.Instantiate(gridDebuggerTransform,spawnTransform.position+ GetWorldPosition3D(gridPosition) , Quaternion.identity);
                if(gridDebuggerTransformInstance.TryGetComponent<GridObjectDebugger>(out GridObjectDebugger debugger))
                {
                    debugger.SetGridObject(GetGridObject(gridPosition));
                }
            }
        }
    }

    public bool IsValidGridPosition(GridPosition gridPosition)
    {
        if(gridPosition.x >= 0 && 
           gridPosition.x < width && 
           gridPosition.z >= 0 && 
           gridPosition.z < height)
        {
            return true;
        }

        return false;
    }

    private int GetWidth() => width;
    private int GetHeight() => height;    
}
