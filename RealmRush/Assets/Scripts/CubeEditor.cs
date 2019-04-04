using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] // makes the script run in the editor mode
[SelectionBase] // makes it easier to select to object in the editor
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{
    // cached references
    Waypoint waypoint;
    int gridSize;
    Vector3Int gridPos;

    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
        gridSize = waypoint.GetGridSize();
    }

    private void Update()
    {
        SnapToGrid();
        UpdateLabel();
    }

    private void SnapToGrid()
    {
        gridPos = waypoint.GetGridPos();
        transform.position = new Vector3(gridPos.x, 0f, gridPos.z);
    }

    private void UpdateLabel()
    {
        var labelTextMesh = GetComponentInChildren<TextMesh>();

        var xPosStr = (gridPos.x / gridSize).ToString();
        var zPosStr = (gridPos.z / gridSize).ToString();

        string labelText = xPosStr + "," + zPosStr;

        labelTextMesh.text = labelText;
        gameObject.name = "Cube " + labelText;
    }
}
