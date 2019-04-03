using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] // makes the script run in the editor mode
[SelectionBase] // makes it easier to select to object in the editor
public class CubeEditor : MonoBehaviour
{
    [Header("Grid Snapping Parameters")]
    [SerializeField] [Range(1f, 20f)] float xGridSize = 10f;
    [SerializeField] [Range(1f, 20f)] float yGridSize = 10f;
    [SerializeField] [Range(1f, 20f)] float zGridSize = 10f;

    void Update()
    {
        SnapPosition();
        UpdateLabel();
    }

    private void SnapPosition()
    {
        Vector3 snapPos;
        snapPos.x = Mathf.RoundToInt(transform.position.x / xGridSize) * xGridSize;
        snapPos.y = Mathf.RoundToInt(transform.position.y / xGridSize) * yGridSize;
        snapPos.z = Mathf.RoundToInt(transform.position.z / xGridSize) * zGridSize;

        transform.position = snapPos;
    }

    private void UpdateLabel()
    {
        var labelTextMesh = GetComponentInChildren<TextMesh>();

        var xPosStr = (transform.position.x / xGridSize).ToString();
        var zPosStr = (transform.position.z / yGridSize).ToString();

        string labelText = xPosStr + "," + zPosStr;

        labelTextMesh.text = labelText;
        gameObject.name = "Cube " + labelText;
    }
}
