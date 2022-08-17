using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridObjectDebugger : MonoBehaviour
{
    [SerializeField] private TextMeshPro debugText;

    private GridObject gridObject;

    public void SetGridObject(GridObject gridObject)
    {
        this.gridObject = gridObject;
        SetupText();
    }

    private void SetupText()
    {
        debugText.text = gridObject.ToString();
    }

}
