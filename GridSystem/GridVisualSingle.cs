using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridVisualSingle : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;

    private GridPosition gridPosition;

    public void SetUp(GridPosition gridPosition)
    {
        this.gridPosition = gridPosition;
    }

    public void Show()
    {
        meshRenderer.enabled = true;
    }

    public void Hide()
    {
        meshRenderer.enabled = false;
    }

    public GridPosition GetGridPosition() => gridPosition;
}
