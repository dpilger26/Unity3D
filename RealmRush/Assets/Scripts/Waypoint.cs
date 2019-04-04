using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    // constants
    const int gridSize = 10;

    public int GetGridSize()
    {
        return gridSize;
    }

    public Vector3Int GetGridPos()
    {
        int xPos = Mathf.RoundToInt(transform.position.x / gridSize) * gridSize;
        int yPos = Mathf.RoundToInt(transform.position.y / gridSize) * gridSize;
        int zPos = Mathf.RoundToInt(transform.position.z / gridSize) * gridSize;

        return new Vector3Int(xPos, yPos, zPos);
    }

    public void SetTopColor(Color color)
    {
        var top = transform.Find("Top");
        top.GetComponent<MeshRenderer>().sharedMaterial.color = color;
    }
}
