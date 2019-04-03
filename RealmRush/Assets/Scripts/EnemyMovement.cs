using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // configuration parameters
    [SerializeField] List<Block> path;

    // Start is called before the first frame update
    void Start()
    {
        PrintAllWaypoints();
    }

    private void PrintAllWaypoints()
    {
        foreach (var block in path)
        {
            Debug.Log(block.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
