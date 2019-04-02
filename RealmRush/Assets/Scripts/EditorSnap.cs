using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] // makes the script run in the editor mode
public class EditorSnap : MonoBehaviour
{
    [SerializeField] [Range(1f, 20f)] float xGridSize = 10f;
    [SerializeField] [Range(1f, 20f)] float yGridSize = 10f;
    [SerializeField] [Range(1f, 20f)] float zGridSize = 10f;

    void Update()
    {
        Vector3 snapPos;
        snapPos.x = Mathf.RoundToInt(transform.position.x / xGridSize) * xGridSize;
        snapPos.y = Mathf.RoundToInt(transform.position.y / xGridSize) * yGridSize;
        snapPos.z = Mathf.RoundToInt(transform.position.z / xGridSize) * zGridSize;

        transform.position = snapPos;
    }
}
