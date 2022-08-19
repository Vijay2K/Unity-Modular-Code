using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance { get; private set; }

    [SerializeField] private int width = 10;
    [SerializeField] private int height = 10;
    [SerializeField] private int cellSize = 2;
    [SerializeField] private Transform gridDebuggerTransform;

    private GridSystem<GridObject> gridSystem;

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
        gridSystem = new GridSystem<GridObject>(width, height, cellSize, 
                (GridSystem<GridObject> gridSystem, GridPosition gridPosition) => new GridObject(gridSystem, gridPosition));

        //gridSystem.CreateGridDebugObject(gridDebuggerTransform);
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return gridSystem.GetGridPosition(worldPosition);
    }

    public Vector3 GetWorldPosition3D(GridPosition gridPosition)
    {
        return gridSystem.GetWorldPosition3D(gridPosition);
    }

    public Vector3 GetWorldPosition2D(GridPosition gridPosition)
    {
        return gridSystem.GetWorldPosition2D(gridPosition);
    }

    public bool GetIsValidGridPosition(GridPosition gridPosition)
    {
        return gridSystem.IsValidGridPosition(gridPosition);
    }

    public int GetWidth()
    {
        return gridSystem.GetWidth();
    }

    public int GetHeight()
    {
        return gridSystem.GetHeight();
    }
}
