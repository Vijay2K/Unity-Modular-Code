using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridVisual : MonoBehaviour
{
    [SerializeField] private Transform gridVisualPrefab;

    private GridVisualSingle[,] gridVisualSingleArray;

    private void Start() 
    {
        gridVisualSingleArray = new GridVisualSingle[
                LevelGrid.Instance.GetWidth(), 
                LevelGrid.Instance.GetHeight()
            ];

        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                Transform gridVisualSingleTransform = Instantiate(gridVisualPrefab, LevelGrid.Instance.GetWorldPosition3D(gridPosition), Quaternion.identity);
                GridVisualSingle gridVisualSingle = gridVisualSingleTransform.GetComponent<GridVisualSingle>();
                gridVisualSingle.SetUp(gridPosition);
                gridVisualSingleArray[x, z] = gridVisualSingle;
            }
        }
    }

    private void Update() 
    {
        GridPosition gridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());
        if(!LevelGrid.Instance.GetIsValidGridPosition(gridPosition)) return;

        UpdateVisual();
    }

    private void UpdateVisual()
    {
        HideGridVisual();

        GridPosition gridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());
        GetGridVisualSingle(gridPosition).Show();
    }

    private GridVisualSingle GetGridVisualSingle(GridPosition gridPosition)
    {
        return gridVisualSingleArray[gridPosition.x, gridPosition.z];
    }

    private void HideGridVisual()
    {
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                gridVisualSingleArray[x, z].Hide();
            }
        }
    }
}
